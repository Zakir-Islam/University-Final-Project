using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_Final_Project.Models;
using University_Final_Project.Repository;

namespace University_Final_Project.Controllers
{
    public class DepartmentController : Controller
    {
        public IDepartmentRepository DepartmentRepository { get; }

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            DepartmentRepository = departmentRepository;
        }
        // GET: DepartmentController
        public async Task<ActionResult> Index()
        {
            return View(await DepartmentRepository.GetAllDepartmentAsync());
        }

        // GET: DepartmentController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var dep = await DepartmentRepository.GetDepartmentAsync(id);
            return View(dep);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
               var dep=await  DepartmentRepository.AddDepartmentAsync(department);
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.Error = "Model state is invalid";
                return View(department);
            }
        }

        // GET: DepartmentController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var dep =await DepartmentRepository.GetDepartmentAsync(id);
            return View(dep);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id,Department department)
        {
            if (ModelState.IsValid)
            {
          
                await DepartmentRepository.UpdateDepartmentAsync(department);
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.Error = "Model state is invalid";
                return View(department);
            }
        }

        // GET: DepartmentController/Delete/5
        public async Task< ActionResult> Delete(string id)
        {

            var dep = await DepartmentRepository.GetDepartmentAsync(id);

            return View(dep);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Delete(string id, Department department)
        {

            await DepartmentRepository.DeleteDepartmentAsync(id);
            return RedirectToAction("index");
        }
    }
}
