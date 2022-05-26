using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class Department
    {
    
        [key]
        [Required]
        [Display(Name ="Id")]
        public string departmentId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string departmentName { get; set; }
      
        public ICollection<Degree> degrees { get; set; }
    }
}
