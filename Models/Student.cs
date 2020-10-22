using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class Student
    {
        [Key]
        public int Student_Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        

        public ICollection<ExamResult> examResults {get;set;}
    }
}
