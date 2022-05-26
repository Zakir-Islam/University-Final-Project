using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using University_Final_Project.Models;
using University_Final_Project.Repository;

namespace University_Final_Project.Controllers
{
    public class AttendenceController : Controller
    {
        public IAttendenceRepository AttendenceRepository { get; }
        public ExamContext ExamContext { get; }
        public IWebHostEnvironment Env { get; }

        public AttendenceController(IAttendenceRepository attendenceRepository,ExamContext examContext
            ,IWebHostEnvironment env)
        {
            AttendenceRepository = attendenceRepository;
            ExamContext = examContext;
            Env = env;
        }
        // GET: AttendeceController
        public async  Task<IActionResult> Index(string subjectID)
        {
            var att = await AttendenceRepository.GetAllAttendencesAsync();
            ViewBag.CardFlag = false;
            if (subjectID != null)
            {
                ViewBag.CardFlag = true;
                att = att.Where(a => a.subjectId == subjectID).ToList();
                ViewBag.TotalAbsence= att.Where(a => a.status == "A" || a.status=="a").Count();
                ViewBag.TotalPresence = att.Where(a => a.status == "P" || a.status == "b").Count();
                ViewBag.TotalLeave = att.Where(a => a.status == "L" || a.status == "l").Count();
                ViewBag.TotalLecture =(float) att.Count();
            }

            return View(att);
        }

        // GET: AttendeceController/Details/5
        public async Task<IActionResult> Details(int id)
        {

            return View( await AttendenceRepository.GetAttendenceAsync(id));
        }

        // GET: AttendeceController/Create
        public ActionResult Create()
        {
            ViewBag.List = new SelectList(ExamContext.Subjects, "Subject_Id", "corse_title");
            return View();
        }

        // POST: AttendeceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Create(IFormFile fileMid,IFormFile fileFinal ,Attendence attendence)
        {
            ViewBag.List = new SelectList(ExamContext.Subjects, "Subject_Id", "corse_title");
            var attendencesMid = readAttendeceFromExcelSheet(fileMid,0);
            var attendencesFinal = readAttendeceFromExcelSheet(fileFinal,16);


            foreach (var attend in attendencesMid)
                {
                    attend.subjectId = attendence.subjectId;
                    await ExamContext.attendences.AddAsync(attend);
                    await ExamContext.SaveChangesAsync();
                    ViewBag.Success = true;
                }

            foreach (var attend in attendencesFinal)
            {
                attend.subjectId = attendence.subjectId;
                await ExamContext.attendences.AddAsync(attend);
                await ExamContext.SaveChangesAsync();
                ViewBag.Success = true;
            }



            return View();
        }

        public List<Attendence> readAttendeceFromExcelSheet(IFormFile file,int startFrom)
        {
            var result = string.Empty;
            string worksheetsName = "data";

            bool firstRowIsHeader = false;
            var format = new ExcelTextFormat();
            format.Delimiter = ',';
            format.TextQualifier = '"';

            var reader = new StreamReader(file.OpenReadStream());
            ExcelPackage package = new ExcelPackage();

            result = reader.ReadToEnd();
            ExcelWorksheet worksheet =
            package.Workbook.Worksheets.Add(worksheetsName);
            worksheet.Cells["A1"].LoadFromText(result, format, OfficeOpenXml.Table.TableStyles.Dark11, firstRowIsHeader);



            int rowCount = worksheet.Dimension.End.Row;
            int colCount = worksheet.Dimension.End.Column;
            int col = 0;
            List<Attendence> attendences = new List<Attendence>();
            string studentRoll = null;
            for (int row = 2; row < rowCount; row++)
            {

                col = 1;

                studentRoll = (string)worksheet.Cells[row, col].Value;
                List<AttendenceChart> attendenceCharts = new List<AttendenceChart>();
                for (int i = 1; i <= 16; i++)
                {
                    ++col;
                    var val2 = worksheet.Cells[row, col].Value;
                    attendenceCharts.Add(new AttendenceChart()
                    {

                        Lecture_No = ( i+startFrom).ToString(),
                        Status = val2.ToString(),

                    });
                }


                if (studentRoll != null)
                {
                    foreach (var att in attendenceCharts)
                    {
                        attendences.Add(new Attendence()
                        {
                            status = att.Status,
                            lectureNo = att.Lecture_No,
                            studentId = studentRoll

                        });
                    }

                }


            }

            return attendences;
        }

        // GET: AttendeceController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var attendence = await AttendenceRepository.GetAttendenceAsync(id);
          
            return View(attendence);

        }

        // POST: AttendeceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Attendence attendence)
        {
            await AttendenceRepository.UpdateAttendenceAsync(attendence);
            return View(attendence);
        }

        // GET: AttendeceController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var attendence = await AttendenceRepository.GetAttendenceAsync(id);

            return View(attendence);
        }

        // POST: AttendeceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await AttendenceRepository.DeleteAttendenceAsync(id);

            return View();
        }
        public async Task<FileResult> DownloadResultTemplate()
        {

            MemoryStream memory = new MemoryStream();
            var rootPath = Env.WebRootPath + "/File/AttendenceTemplate.csv";
            var fileStream = new FileStream(rootPath, FileMode.Open);

            await fileStream.CopyToAsync(memory);
            memory.Position = 0;
            fileStream.Close();
            return File(memory, "text/xlxs", rootPath);

        }

        public async Task<IActionResult> StudentAttendenceDash(string std_id)
        {
          
            var attendences = await ExamContext.attendences.Where(s => s.studentId==std_id).ToListAsync();
            var subjects =new  List<Subject>();
            foreach(var att in attendences)
            {
                var subj = await ExamContext.Subjects.FindAsync(att.subjectId);
                if (subjects.Contains(subj))
                {
                    continue;
                }
                subjects.Add( subj);
            }
            return View(subjects);


        }
    }
}
