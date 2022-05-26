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
    public class Teacher
    {
        [Key]
        [Required]
        [Display(Name ="Employee Id")]
        public string Employee_Id { get; set; }
      
        [Required]
        [Display(Name ="First Name")]
        public string First_Name {get;set;}
        [Required]
        [Display(Name ="Last Name")]
        public string Last_Name { get; set; }
        [Required]
        [Display(Name ="Father Name")]
        public string Father_Name { get; set; }
      

    }
}
