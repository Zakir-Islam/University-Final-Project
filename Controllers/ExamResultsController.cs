using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University_Final_Project.Models;
using System.ComponentModel;
using University_Final_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace University_Final_Project.Controllers
{
    public class ExamResultsController : Controller
    {
        private readonly ExamContext _context;

        public IWebHostEnvironment Env;
        private readonly GradeCalculator gradeCalculator;
        
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SubjectRepository subjectRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IExamResultRepository examResultRepository;

        public ExamResultsController(ExamContext context, IWebHostEnvironment env,
            GradeCalculator gradeCalculator, UserManager<ApplicationUser> userManager
            , SubjectRepository subjectRepository, IStudentRepository studentRepository,
            IExamResultRepository examResultRepository)
        {
            _context = context;
            Env = env;
            this.gradeCalculator = gradeCalculator;
         
            this.userManager = userManager;
            this.subjectRepository = subjectRepository;
            this.studentRepository = studentRepository;
            this.examResultRepository = examResultRepository;
        }

        // GET: ExamResults
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Index(string searchString,string ErrorMessage)
        {

            ViewBag.Error = ErrorMessage;

            var examResults = await _context.ExamResults.OrderBy(e => e.semester).ToListAsync();
            if (searchString != null)
            {
                 var results= await _context.ExamResults.Include(e=>e.student).Include(e=>e.subject)
                    .Where(e=>e.Student_FId==searchString)
                  .OrderBy(e => e.semester).ToListAsync();
               
            }
        

            examResults = gradeCalculator.calculateGpa(examResults);
            return View(examResults);




        }

        // GET: ExamResults/Details/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults
                .FirstOrDefaultAsync(m => m.Exam_Id== id);

         
            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        // GET: ExamResults/Create
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Create()
        {
            ViewBag.List = new SelectList(_context.Subjects, "Subject_Id", "corse_title");
            return View();
        }

        // POST: ExamResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(IFormFile file, ExamResult exam, int semester)
        {

            ViewBag.List = new SelectList(_context.Subjects, "Subject_Id", "corse_title");

              var result = string.Empty;
                string worksheetsName = "data";

                bool firstRowIsHeader = false;
                var format = new ExcelTextFormat();
                format.Delimiter = ',';
                format.TextQualifier = '"';

                var reader = new StreamReader(file.OpenReadStream()) ;
                ExcelPackage package = new ExcelPackage();

                    result = reader.ReadToEnd();
                    ExcelWorksheet worksheet =
                    package.Workbook.Worksheets.Add(worksheetsName);
                    worksheet.Cells["A1"].LoadFromText(result, format, OfficeOpenXml.Table.TableStyles.Medium27, firstRowIsHeader);



                int rowCount = worksheet.Dimension.End.Row;
                int colCount = worksheet.Dimension.End.Column;
                int col = 0;
                List<ExamResult> examresults = new List<ExamResult>();
                for (int row = 2; row <= rowCount; row++)
                {


                    col = 1;



                        var val1 = worksheet.Cells[row, col].Value;
                    ++col;
                        var val2 = worksheet.Cells[row, col].Value;
                 
                
                    if (val1 != null && val2 != null) {
                        examresults.Add(new ExamResult()
                        {

                            Student_FId = val1.ToString(),
                            Marks = (double)val2

                        });
                    }


                }


                foreach (var item in examresults)
                {
                    item.subject_FId = exam.subject_FId;
                    item.semester = semester;
                    _context.ExamResults.Add(item);
                    _context.SaveChanges();
                    ViewBag.Success = true;
                }
          
            return View();
        }



        // GET: ExamResults/Edit/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.list = new SelectList(_context.Subjects.ToList(), "Subject_Id", "corse_title");
            var examResult = await examResultRepository.FindExamResultAsync(id);
            if (examResult == null)
            {
                return NotFound();
            }
            return View(examResult);
        }

        // POST: ExamResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int id,  ExamResult examResult,int semester)
        {
            if (id != examResult.Exam_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    examResult.semester = semester;
                    _context.Update(examResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamResultExists(examResult.Exam_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(examResult);
        }

        // GET: ExamResults/Delete/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await examResultRepository.FindExamResultAsync(id);
            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        // POST: ExamResults/Delete/5
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await examResultRepository.DeleteExamResultAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> ViewTranscript(string filterString,string std_id)
        {
            ViewBag.std_id = User.Identity.Name;
            if (std_id == null)
            {
                std_id = User.Identity.Name;
            }
            TranscriptWithStudent transcriptWithStudent = new TranscriptWithStudent();
            List<Transcript> transcript = new List<Transcript>();
            var results = await _context.ExamResults.Where(e =>
                           e.Student_FId == std_id).AsNoTracking().OrderBy(e => e.semester).ToListAsync();
            var res = new List<ExamResult>();
            var semestercount = results.Max(a => a.semester);
            var semTosemRes = new List<ExamResult>();

            for (int i = 1; i <= semestercount; i++)
            {
                res = results.Where(e =>
                       e.semester == i).ToList();

                foreach (var item in res)
                {
                    item.subject = await subjectRepository.GetSubjectAsync(item.subject_FId);
                    semTosemRes.Add(item);
                }
                res = gradeCalculator.calculateGpa(res);
                var gpa = (float)gradeCalculator.calculateCgpa(res);
                var cgpa = (float)gradeCalculator.calculateCgpa(semTosemRes);
                var TCrHrs = gradeCalculator.TotalCrHrs(res);
                transcript.Add(new Transcript()
                {
                    examResults = res,
                    semester = i,
                    GPA = gpa,
                    CGPA = cgpa,
                    CrHrs = TCrHrs

                });

            }



            var student = await studentRepository.GetStudentAsync(std_id);
            student.degree = await _context.Degrees.FindAsync(student.DegreeFid);
            student.degree.department = await _context.Departments.FindAsync(student.degree.departmentFid);
            transcriptWithStudent.transcripts = transcript;
            transcriptWithStudent.student = student;
            transcriptWithStudent.transcripts = transcript;
            transcriptWithStudent.student = student;
            if (filterString != null)
            {
                var subj_name = subjectRepository
                 .getSubjectId(filterString);
                if (subj_name != null)
                {
                    results = results.Where(r => r.subject_FId == subj_name).ToList();
                }
                else
                {
                    ViewBag.Error = "No Subject Found with name " + filterString;
                }

            }


            if (results.Count > 0)
            {
                results = gradeCalculator.calculateGpa(results);

                ViewBag.CGPA = (float)gradeCalculator.calculateCgpa(results);

                return View(transcriptWithStudent);
            }
            var ErrorMessage = "No Student Found with ID = " + std_id;
            return RedirectToAction("index", new { ErrorMessage = ErrorMessage });

        }
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> AdminViewTranscript( string std_id,string filterString)
        {

            TranscriptWithStudent transcriptWithStudent = new TranscriptWithStudent();
            List<Transcript> transcript = new List<Transcript>();
             var results = await _context.ExamResults.Where(e =>
                            e.Student_FId == std_id).AsNoTracking().OrderBy(e => e.semester).ToListAsync();
            var res = new List<ExamResult>();
            var semestercount = results.Max(a => a.semester);
            var semTosemRes = new List<ExamResult>();

            for(int i = 1; i <= semestercount; i++)
            {
                res= results.Where(e =>
                      e.semester == i).ToList();

                foreach (var item in res)
                {
                    item.subject = await subjectRepository.GetSubjectAsync(item.subject_FId);
                    semTosemRes.Add(item);
                }
                res = gradeCalculator.calculateGpa(res);
                var gpa =(float) gradeCalculator.calculateCgpa(res);
                var cgpa = (float)gradeCalculator.calculateCgpa(semTosemRes);
                var TCrHrs = gradeCalculator.TotalCrHrs(res);
                transcript.Add(new Transcript()
                {
                    examResults = res,
                    semester = i,
                    GPA=gpa,
                    CGPA=cgpa,
                    CrHrs=TCrHrs
                                       
                });                      

            }


            
            var student =await studentRepository.GetStudentAsync(std_id);
            student.degree =await _context.Degrees.FindAsync(student.DegreeFid);
            student.degree.department = await _context.Departments.FindAsync(student.degree.departmentFid);
            transcriptWithStudent.transcripts = transcript;
            transcriptWithStudent.student = student;
            if (filterString != null)
            {
                var subj_name = subjectRepository
                 .getSubjectId(filterString);
                if (subj_name != null)
                {
                    results = results.Where(r => r.subject_FId ==subj_name ).ToList();
                }
                else
                {
                    ViewBag.Error = "No Subject Found with name " + filterString;
                }
                
            }
          

            if (results.Count>0)
            {
                results = gradeCalculator.calculateGpa(results);
               
                ViewBag.CGPA = (float)gradeCalculator.calculateCgpa(results);
              
                return View(transcriptWithStudent);
            }
             var ErrorMessage= "No Student Found with ID = " + std_id;
            return RedirectToAction("index",new { ErrorMessage=ErrorMessage});



        }
         [Authorize(Roles = "Admin,Teacher")]
        public async Task<FileResult> DownloadResultTemplate()
        {

            MemoryStream memory = new MemoryStream();
            var rootPath = Env.WebRootPath + "/File/ResultTemplate.csv";
            var fileStream=new FileStream(rootPath,FileMode.Open) ;
          
            await fileStream.CopyToAsync(memory);
            memory.Position = 0;
            fileStream.Close();
            return File(memory, "text/xlxs", rootPath);
          
        }

        private bool ExamResultExists(int id)
        {
            return _context.ExamResults.Any(e => e.Exam_Id == id);
        }

        public async Task<IActionResult> Transcript()
        {
            var examresult = new ExamResult();
            return View(examresult);
        }
    }
}
