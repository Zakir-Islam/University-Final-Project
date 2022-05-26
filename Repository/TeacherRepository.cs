using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ExamContext examContext;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleManager<IdentityRole> RoleManager { get; }

        public TeacherRepository(ExamContext examContext, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.examContext = examContext;
            this.userManager = userManager;
            RoleManager = roleManager;
        }

        public async Task<Teacher> AddTeacherAsync(Teacher Teacher)
        {
            await examContext.Teachers.AddAsync(Teacher);
            var applicationUser = new ApplicationUser()
            {
                UserName = Teacher.Employee_Id,
                teacher_Fid = Teacher.Employee_Id,
            };

            var result = await userManager.CreateAsync(applicationUser, "admin1");
            if (await RoleManager.RoleExistsAsync("Teacher"))
            {
                await userManager.AddToRoleAsync(applicationUser, "Teacher");
            }


            await examContext.SaveChangesAsync();
            return Teacher;

        }

        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await examContext.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetTeacherAsync(string Id)
        {
            var Teacher = await examContext.Teachers.FindAsync(Id);

            return Teacher;

        }

        public async Task<Teacher> DeleteTeacherAsync(string id)
        {
            var Teacher = examContext.Teachers.Find(id);
            var user = await userManager.FindByNameAsync(id);


            await userManager.RemoveFromRoleAsync(user, "Teacher");
            await userManager.DeleteAsync(user);
            examContext.Teachers.Remove(Teacher);
            await examContext.SaveChangesAsync();
            return Teacher;
        }

        public async Task<Teacher> UpdateTeacherAsync(Teacher Teacher)
        {
            examContext.Teachers.Update(Teacher);
            await examContext.SaveChangesAsync();

            return Teacher;
        }

        public async Task<Teacher> FindTeacherAsync(string id)
        {
            var Teacher = await examContext.Teachers.FindAsync(id);


            return Teacher;
        }

    }
}
