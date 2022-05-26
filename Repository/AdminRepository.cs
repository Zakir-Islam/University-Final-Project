using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ExamContext examContext;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleManager<IdentityRole> RoleManager { get; }

        public AdminRepository(ExamContext examContext, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.examContext = examContext;
            this.userManager = userManager;
            RoleManager = roleManager;
        }

        public async Task<Admin> AddAdminAsync(Admin Admin)
        {
            await examContext.Admins.AddAsync(Admin);
            var applicationUser = new ApplicationUser()
            {
                UserName = Admin.Employee_Id,
                admin_Fid = Admin.Employee_Id,
            };

            var result = await userManager.CreateAsync(applicationUser, "admin1");
            if (await RoleManager.RoleExistsAsync("Admin"))
            {
                await userManager.AddToRoleAsync(applicationUser, "Admin");
            }


            await examContext.SaveChangesAsync();
            return Admin;

        }

        public async Task<List<Admin>> GetAllAdminsAsync()
        {
            return await examContext.Admins.ToListAsync();
        }

        public async Task<Admin> GetAdminAsync(string Id)
        {
            var Admin = await examContext.Admins.FindAsync(Id);

            return Admin;

        }

        public async Task<Admin> DeleteAdminAsync(string id)
        {
            var Admin = examContext.Admins.Find(id);
            var user = await userManager.FindByNameAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Teacher");
            await userManager.DeleteAsync(user);
            examContext.Admins.Remove(Admin);
            await examContext.SaveChangesAsync();
            return Admin;
        }

        public async Task<Admin> UpdateAdminAsync(Admin Admin)
        {
            examContext.Admins.Update(Admin);
            await examContext.SaveChangesAsync();

            return Admin;
        }

        public async Task<Admin> FindAdminAsync(string id)
        {
            var Admin = await examContext.Admins.FindAsync(id);


            return Admin;
        }

    }
}
