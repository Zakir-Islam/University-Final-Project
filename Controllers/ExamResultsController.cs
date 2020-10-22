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

namespace University_Final_Project.Controllers
{
    public class ExamResultsController : Controller
    {
        private readonly ExamContext _context;

        public IWebHostEnvironment Env;
        private readonly GradeCalculator gradeCalculator;
        private readonly StudentRepository studentRepository;

        public ExamResultsController(ExamContext context, IWebHostEnvironment env,
            GradeCalculator gradeCalculator,StudentRepository studentRepository)
        {
            _context = context;
            Env = env;
            this.gradeCalculator = gradeCalculator;
            this.studentRepository = studentRepository;
        }

        // GET: ExamResults
        public async Task<IActionResult> Index()
        {
            var examResults = await _context.ExamResults.ToListAsync();

            foreach(var item in examResults)
            {
                item.student_name = studentRepository.geStudentName(item.Student_FId);
            }
          
            examResults = gradeCalculator.calculateGpa(examResults);
            return View(examResults);
        }

        // GET: ExamResults/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExamResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file)
        {
            var Folder = "/File/";
           var FileUrl = Folder + Guid.NewGuid().ToString() + file.FileName;
            var rootPath = Env.WebRootPath + FileUrl;
            file.CopyTo(new FileStream(rootPath,FileMode.Create));

            var examreults = getResults(FileUrl,file);

            foreach(var item in examreults)
            {
                _context.ExamResults.Add(item);
                _context.SaveChanges();
            }

           


            return View();
        }

        private List<ExamResult> getResults(string fileUrl,IFormFile file)
        {
            List<ExamResult> examResults = new List<ExamResult>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var stream = file.OpenReadStream();
            var reader = ExcelReaderFactory.CreateReader(stream);

            while (reader.Read())
            {
                examResults.Add(new ExamResult()
                {
                    Student_FId =Convert.ToInt32(reader.GetValue(0)),
                    subject_FId =Convert.ToInt32(reader.GetValue(1)),
                    mid = (double)reader.GetValue(2),
                    final = (double)reader.GetValue(3),
                    sessional = (double)reader.GetValue(4),
                });

            }
            

            return examResults;
        }

        // GET: ExamResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults.FindAsync(id);
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
        
        public async Task<IActionResult> Edit(int id,  ExamResult examResult)
        {
            if (id != examResult.Exam_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
        public async Task<IActionResult> Delete(int? id)
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

        // POST: ExamResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examResult = await _context.ExamResults.FindAsync(id);
            _context.ExamResults.Remove(examResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamResultExists(int id)
        {
            return _context.ExamResults.Any(e => e.Exam_Id == id);
        }
    }
}
