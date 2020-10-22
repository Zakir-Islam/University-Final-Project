using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class ExamResult
    {
        [Key]
        public int  Exam_Id { get; set; }

        public int Student_FId { get; set; }
        public int subject_FId{ get; set; }
        [NotMapped ]
        public string student_name { get; set; }
      
        public double mid { get; set; }
        public double final { get; set; }
        public double sessional { get; set; }
        [NotMapped]
        public double gpa { get; set; }

        public Subject subject { get; set; }

        public Student Student { get; set; }




        

    }
}
