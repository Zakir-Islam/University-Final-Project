using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class Degree
    {

        [key]
        [Required]
        [Display(Name = "Id")]
        public string DegreeId { get; set; }

        [Required]
        [Display(Name = "Department")]
        public string departmentFid { get; set; }
        [Required]
        [Display(Name = "Degree Name")]
        public string DegreeName { get; set; }

        public Department department { get; set; }
        public ICollection<Student> students { get; set; }
    }
}
