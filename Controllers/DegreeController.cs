using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University_Final_Project.Models;
using University_Final_Project.Repository;

namespace University_Final_Project.Controllers
{
    public class DegreeController : Controller
    {
        public IDegreeRepository DegreeRepository { get; }
        public ExamContext ExamContext { get; }

        public DegreeController(IDegreeRepository degreeRepository,ExamContext examContext)
        {
            DegreeRepository = degreeRepository;
            ExamContext = examContext;
        }
        // GET: DegreeController
        public async Task<ActionResult> Index()
        {
            return View(await DegreeRepository.GetAllDegreeAsync());
        }

        // GET: DegreeController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var dep = await DegreeRepository.GetDegreeAsync(id);
            return View(dep);
        }

        // GET: DegreeController/Create
        public ActionResult Create()
        {
            ViewBag.list = new SelectList(ExamContext.Departments, "departmentId", "departmentName");
            return View();
        }

        // POST: DegreeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Degree Degree)
        {
            ViewBag.list = new SelectList(ExamContext.Departments, "departmentId", "departmentName");
            if (ModelState.IsValid)
            {
                var dep = await DegreeRepository.AddDegreeAsync(Degree);
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.Error = "Model state is invalid";
                return View(Degree);
            }
        }

        // GET: DegreeController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            ViewBag.list = new SelectList(ExamContext.Departments, "departmentId", "departmentName");
            var dep = await DegreeRepository.GetDegreeAsync(id);
            return View(dep);
        }

        // POST: DegreeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, Degree Degree)
        {
            ViewBag.list = new SelectList(ExamContext.Departments, "departmentId", "departmentName");
            if (ModelState.IsValid)
            {

                await DegreeRepository.UpdateDegreeAsync(Degree);
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.Error = "Model state is invalid";
                return View(Degree);
            }
        }

        // GET: DegreeController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {

            var dep = await DegreeRepository.GetDegreeAsync(id);

            return View(dep);
        }

        // POST: DegreeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, Degree Degree)
        {

            await DegreeRepository.DeleteDegreeAsync(id);
            return RedirectToAction("index");
        }
    }
}
