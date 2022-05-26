using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University_Final_Project.Models;
using University_Final_Project.Repository;

namespace University_Final_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        public ITeacherRepository TeacherRepository { get; }
        public TeacherController(ITeacherRepository TeacherRepository)
        {
            this.TeacherRepository = TeacherRepository;
        }



        public async Task<IActionResult> Index()
        {
            var Teachers = await TeacherRepository.GetAllTeachersAsync();
            return View(Teachers);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Teacher Teacher)
        {
            var T = await TeacherRepository.AddTeacherAsync(Teacher);
            return View(T);
        }
        public async Task<IActionResult> Details(string id)
        {
            var std = await TeacherRepository.GetTeacherAsync(id);
            return View(std);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var std = await TeacherRepository.FindTeacherAsync(id);
            return View(std);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await TeacherRepository.DeleteTeacherAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var std = await TeacherRepository.FindTeacherAsync(id);
            return View(std);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Teacher Teacher)
        {
            var std = await TeacherRepository.UpdateTeacherAsync(Teacher);
            return View(std);
        }
    }
}
