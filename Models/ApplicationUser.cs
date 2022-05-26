using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace University_Final_Project.Models
{
    public class ApplicationUser:IdentityUser
    {
           
            public string student_Fid { get; set; }
            public string teacher_Fid { get; set; }
            public string admin_Fid { get; set; }
            public Student Student { get; set; }
            public Teacher Teacher { get; set; }



    }
}
