using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University_Final_Project.Models;
using University_Final_Project.Repository;

namespace University_Final_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        public IStudentRepository StudentRepository { get; }
        public ExamContext Exam { get; }

        public StudentController(IStudentRepository studentRepository,ExamContext exam)
        {
            StudentRepository = studentRepository;
            Exam = exam;
        }

     

        public async Task<IActionResult> Index()
        {
            var students = await StudentRepository.GetAllStudentsAsync();
            return View(students);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.list = new SelectList(Exam.Degrees, "DegreeId", "DegreeName");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            ViewBag.list = new SelectList(Exam.Degrees, "DegreeId", "DegreeName");
            var std =await  StudentRepository.AddStudentAsync(student);
            return View(std);
        }
        public async Task< IActionResult> Details(string id)
        {
            var std =await  StudentRepository.GetStudentAsync(id);
            return View(std);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var std = await StudentRepository.FindStudentAsync(id);
            return View(std);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await StudentRepository.DeleteStudentAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.list = new SelectList(Exam.Degrees, "DegreeId", "DegreeName");
            var std = await StudentRepository.FindStudentAsync(id);
            return View(std);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            ViewBag.list = new SelectList(Exam.Degrees, "DegreeId", "DegreeName");
            var std = await StudentRepository.UpdateStudentAsync(student);
            return View(std);
        }
    }
}
