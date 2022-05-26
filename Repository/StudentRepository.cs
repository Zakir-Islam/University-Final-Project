using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ExamContext examContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public StudentRepository(ExamContext examContext,UserManager<ApplicationUser> userManager
            ,RoleManager<IdentityRole> roleManager)
        {
            this.examContext = examContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            await examContext.Students.AddAsync(student);
            var applicationUser = new ApplicationUser()
            {
                UserName = student.Roll_Number,
                student_Fid = student.Roll_Number,
            };

            var result=await userManager.CreateAsync(applicationUser, "admin1");
            await userManager.AddToRoleAsync(applicationUser,"Student");
           
            await examContext.SaveChangesAsync();
            return student;

        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await examContext.Students.ToListAsync();
        }

        public async Task<Student> GetStudentAsync(string Id)
        {
            var student = await examContext.Students.FindAsync(Id);

            return student;

        }

        public async Task<Student> DeleteStudentAsync(string id)
        {
            var student = examContext.Students.Find(id);
            var user = await userManager.FindByNameAsync(id);
            

            await userManager.RemoveFromRoleAsync(user, "Student");
            await userManager.DeleteAsync(user);
            examContext.Students.Remove(student);
            await examContext.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            examContext.Students.Update(student);
            await examContext.SaveChangesAsync();

            return student;
        }

        public async Task<Student> FindStudentAsync(string id)
        {
           var student= await examContext.Students.FindAsync(id);
           

            return student;
        }

        public async Task<string> GetFullNameAsync(string id)
        {
            var student = await examContext.Students.FindAsync(id);
            return student.First_Name + " " + student.Last_Name;
        }
    }
}
