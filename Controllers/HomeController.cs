using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using University_Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
namespace University_Final_Project.Controllers
{
   [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExamContext examContext;

        public HomeController(ILogger<HomeController> logger,ExamContext examContext)
        {
            _logger = logger;
            this.examContext = examContext;
        }

        public IActionResult Index()
        {
            var model = new DashboardCounter();
            model.NoAdm = examContext.Admins.Count();
            model.NoStd = examContext.Students.Count();
            model.NoTch = examContext.Teachers.Count();
            model.NoDegr = examContext.Degrees.Count();
            model.NoDep = examContext.Departments.Count();
            model.NoSub = examContext.Subjects.Count();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
