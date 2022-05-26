using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using University_Final_Project.Models;

namespace University_Final_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManager<ApplicationUser> userManager { get; }

        public AdministratorController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel createRoleModel)
        {
            if (ModelState.IsValid)
            {
                var User_Role = new IdentityRole()
                {
                    Name=createRoleModel.Role_Name
                };
                IdentityResult result = await  roleManager.CreateAsync(User_Role);
                if (result.Succeeded)
                {
                    ViewBag.success = true;
                    return View();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            
            return View(createRoleModel);
        }

        [HttpGet]
        public IActionResult ViewList(string searchString)
        {
            var roles = roleManager.Roles;
            if (searchString != null)
            {
                List<IdentityRole> lists = roles.Where(r => r.Name == searchString).ToList();
                return View(lists);
            }
            
            return View(roles);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role =await roleManager.FindByIdAsync(id);
           

            if (role ==null)
            {
                ViewBag.errormessage = $"{id} not found";
                return View("not found");
            }

            var model = new EditRoleViewModel
            {
                id = role.Id,
                Role_Name=role.Name
            };
            

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel editRoleViewModel )
        {
            var role = await roleManager.FindByIdAsync(editRoleViewModel.id);


            if (role == null)
            {
                ViewBag.errormessage = $"{editRoleViewModel.id} not found";
                return View("not found");
            }
            else
            {
                role.Name = editRoleViewModel.Role_Name;
                 var result=await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ViewList", "Administrator");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            
            return View(editRoleViewModel);
        }
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var rolemodel = new CreateRoleModel()
            {
                Role_ID = role.Id,
                Role_Name = role.Name
            };

            return View(rolemodel);
        }

        // POST: ExamResults/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(CreateRoleModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Role_ID);
            await roleManager.DeleteAsync(role);
         
            return RedirectToAction(nameof(ViewList));
        }
    }
}
