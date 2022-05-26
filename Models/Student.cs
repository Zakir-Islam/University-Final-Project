using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class Student
    {
        [Key]
        [Required]
        [Display(Name ="Roll No")]
        public string Roll_Number { get; set; }
        [Required]
        [Display(Name ="Degree")]
        public string DegreeFid { get; set; }
        [Required]
        [Display(Name ="Reg No")]
        public string Reg_Number { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string First_Name {get;set;}
        [Required]
        [Display(Name ="Last Name")]
        public string Last_Name { get; set; }
        [Required]
        [Display(Name ="Father Name")]
        public string Father_Name { get; set; }
        [Required]
        [Display(Name ="Session Start")]
        public int Session_Start { get; set; }

        
        public Degree degree { get; set; }
     
        public ICollection<ExamResult> examResults { get; set; }
    }
}
