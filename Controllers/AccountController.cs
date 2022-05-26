using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University_Final_Project.Models;

namespace University_Final_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        /*
        ==================================================
        Registration BLock
        ===================================================
         */
        /*    
                public async Task<IActionResult> Register()
                {
                    var list = roleManager.Roles;

                    ViewBag.List= new SelectList(list,"Id","Name");
                    return View();
                }

                [HttpPost]
                public async Task<IActionResult> Register(RegisterModel registerModel)
                {

                    var list = roleManager.Roles;

                    ViewBag.List = new SelectList(list, "Id", "Name");

                    if (registerModel.password == registerModel.confirmPassword)
                    {
                        var user = new ApplicationUser()
                        {
                            UserName = registerModel.UserName,
                            FirstName=registerModel.FirstName,
                            LastName=registerModel.LastName

                        };
                        var role = await roleManager.FindByIdAsync(registerModel.role_id);
                        var result = await userManager.CreateAsync(user, registerModel.password);
                        var role_result = await userManager.AddToRoleAsync(user,role.Name);
                        if (result.Succeeded)
                        {
                            ViewBag.success = true;

                            return View();
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        foreach (var error in role_result.Errors)
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

        ==================================================
        Service BLock
        ===================================================

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

                }

             ==================================================
             Login BLock
             ===================================================
            */
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
        /*    
           ==================================================
           View List  BLock
           ===================================================

              public async Task<IActionResult> Index(string searchString)
              {

                  var allUsers = userManager.Users;
                  List<RegisterModel> registeredUsers = new List<RegisterModel>();

                  if (allUsers != null)
                  {
                      foreach(var user in allUsers)
                      {
                          registeredUsers.Add(new RegisterModel()
                          {
                              FirstName=user.FirstName,
                              LastName=user.LastName,
                              user_id=user.Id,
                              UserName=user.UserName,

                          });
                      }
                      if (searchString != null)
                      {
                         registeredUsers= registeredUsers.Where(u => u.UserName == searchString).ToList();
                      }
                      return View(registeredUsers);
                  }



                  ModelState.AddModelError("", "Nthing found");

                  return View();

              }

      ==================================================
      Logout  BLock
      ===================================================
         */
        [AllowAnonymous]
        public async Task<IActionResult> Logout(ApplicationUser user)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn", "Account");
        }
        /*
                public async Task<IActionResult> EditUser( string id)
                {
                    if (id != null)
                    {
                        var user = await userManager.FindByIdAsync(id);



                        if (user != null)
                        {
                            RegisterModel registerModel = new RegisterModel()
                            {
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                UserName = user.UserName,                      
                                user_id=user.Id,

                            };
                            return View(registerModel);

                        }
                    }

                    return View("No Record");

                }

        ==================================================
        Edit user  BLock
        ===================================================

                [HttpPost]
                public async Task<IActionResult> EditUser(RegisterModel registerModel)
                {

                  /*    var user=await userManager.FindByIdAsync(registerModel.user_id);

                        user.UserName = registerModel.UserName;
                        user.FirstName = registerModel.FirstName;
                       user.LastName = registerModel.LastName;





                        var result = await userManager.UpdateAsync(user);


                        if (result.Succeeded)
                        {
                            return RedirectToAction("index");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }





                    return View(registerModel);
                }
                ===============================================
        Details BLock
        ===================================================

                public async Task<IActionResult> UserDetail(string id)
                {
                 /*   if (id != null)
                    {
                        var user = await userManager.FindByIdAsync(id);



                        if (user != null)
                        {
                            RegisterModel registerModel = new RegisterModel()
                            {
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                UserName = user.UserName,
                                user_id = user.Id,

                            };
                            return View(registerModel);

                        }
                    }

                    return View("No Record");

                }

        ==================================================
         Delete user  BLock
        ===================================================

                public async Task<IActionResult> Delete(string id)
                {
                  /  if (id != null)
                    {
                        var user = await userManager.FindByIdAsync(id);



                        if (user != null)
                        {
                            RegisterModel registerModel = new RegisterModel()
                            {
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                UserName = user.UserName,
                                user_id = user.Id,

                            };
                            return View(registerModel);

                        }
                    }

                    return View("No Record");

                }
                [HttpPost,ActionName("Delete")]
                public async Task<IActionResult> DeleteConfirmed(string id)
                {
                    if (id != null)
                    {
                        var user = await userManager.FindByIdAsync(id);
                        await userManager.DeleteAsync(user);

                        return RedirectToAction("index");

                    }

                    return View("No Record");

                }

               ==================================================
               View roles of user BLock
               ===================================================

                public async Task<IActionResult> ViewRoles(string id)
                {

                    var user = await userManager.FindByIdAsync(id);

                    var roles = await userManager.GetRolesAsync(user);

                    RemoveUserRoleModel removeUserRoleModel = new RemoveUserRoleModel()
                    {
                        user_id = id,
                        roles = roles
                    };

                    return View(removeUserRoleModel);

                }

          ==================================================
         Remove  role of user BLock
          ===================================================

                public async Task<IActionResult> Remove(string name,string user_id)
                {

                    var role = await roleManager.FindByNameAsync(name);

                    var user = await userManager.FindByIdAsync(user_id);

                   await userManager.RemoveFromRoleAsync(user,name);

                    return RedirectToAction("index");

                }

         ==================================================
          Assign Role To user BLock
          ===================================================

                public async Task<IActionResult> AssignRole( string user_id)
                {
                    var list = roleManager.Roles;

                    ViewBag.List = new SelectList(list, "Id", "Name");

                    AssignRoleModel assignRoleModel = new AssignRoleModel()
                    { 
                        user_id=user_id,
                    };



                    return View(assignRoleModel);

                }
                [HttpPost]
                public async Task<IActionResult> AssignRole(AssignRoleModel assignRoleModel)
                {
                    if (assignRoleModel.role_id != null && assignRoleModel.user_id != null)
                    {
                        var user = await userManager.FindByIdAsync(assignRoleModel.user_id);
                        var role = await roleManager.FindByIdAsync(assignRoleModel.role_id);
                        if (user != null&&role!=null)
                        {
                            await userManager.AddToRoleAsync(user, role.Name);
                            return RedirectToAction("index");
                        }
                    }


                    return View();

                }

               ==================================================
                Change user password BLock
               ===================================================
            */
        public async Task<IActionResult> ChangeUserPassword()
        {
          
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordModel changeUserPassword)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var user = await userManager.FindByNameAsync(userName);
                await userManager.ChangePasswordAsync(user, changeUserPassword.currentPassword
                    , changeUserPassword.NewPassword);
                return RedirectToAction("index", "home");
            }


            return View();

        }
    }
}
