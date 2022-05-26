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
    public class AdminController : Controller
    {
        public IAdminRepository AdminRepository { get; }
        public AdminController(IAdminRepository AdminRepository)
        {
            this.AdminRepository = AdminRepository;
        }



        public async Task<IActionResult> Index()
        {
            var Admins = await AdminRepository.GetAllAdminsAsync();
            return View(Admins);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Admin Admin)
        {
            if (ModelState.IsValid)
            {
                var T = await AdminRepository.AddAdminAsync(Admin);
                return View(T);
            }

            return View(Admin);
        }
        public async Task<IActionResult> Details(string id)
        {
            var std = await AdminRepository.GetAdminAsync(id);
            return View(std);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var std = await AdminRepository.FindAdminAsync(id);
            return View(std);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await AdminRepository.DeleteAdminAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var std = await AdminRepository.FindAdminAsync(id);
            return View(std);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Admin Admin)
        {
            var std = await AdminRepository.UpdateAdminAsync(Admin);
            return View(std);
        }
    }
}
