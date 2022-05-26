using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class Attendence
    {
        [key]
        public int id { get; set; }
        [Required]
        [Display(Name ="Student Roll")]
        public string studentId { get; set; }
        [Required]
        [Display(Name = "Subject Code")]
        public string subjectId { get; set; }
        [Required]
        [Display(Name = "Lecture No")]
        public string lectureNo { get; set; }
        [Required]
        [Display(Name = "Attendance Status")]
        public string status { get; set; }
    }
}
