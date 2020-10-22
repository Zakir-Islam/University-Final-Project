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

        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<Subject>  Subjects{ get; set; }
        public DbSet<Student> Students { get; set; }
      
    }
}
