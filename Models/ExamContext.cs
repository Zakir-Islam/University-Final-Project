using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Models
{
    public class ExamContext:IdentityDbContext<ApplicationUser>
    {
        public ExamContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var fk in builder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
           
        }
      

        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<Subject>  Subjects{ get; set; }      
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Degree> Degrees { get; set; }

        public DbSet<Attendence> attendences { get; set; }


    }
}
