using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using University_Final_Project.Models;

namespace University_Final_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (registerModel.password == registerModel.confirmPassword)
            {
                var user = new ApplicationUser()
                {
                    UserName = registerModel.UserName,

                };
                var result = await userManager.CreateAsync(user, registerModel.password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", "Password not matched");
            }
            return View(registerModel);
        }
        [HttpPost][HttpGet]
        public async Task<IActionResult> IsEmailTaken(string email)
        {
            var user = userManager.FindByNameAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} already taken");
            }
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> LogIn()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInModel logInModel)
        {
            if (ModelState.IsValid)
            {
                var result=await signInManager.PasswordSignInAsync(logInModel.UserName, logInModel.password,
                    logInModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            ModelState.AddModelError("", "INvalid user name or Password");
            return View(logInModel);
        }

        public async Task<IActionResult> Logout(ApplicationUser user)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn", "Account");
        }

    }
}
