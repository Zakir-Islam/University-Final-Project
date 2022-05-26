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

        public string Student_FId { get; set; }
        public string subject_FId{ get; set; }
       
        public  int  semester { get; set; }
        public double Marks { get; set; }
        [NotMapped]
        public double gpa { get; set; }
        [NotMapped]
        public string Grade { get; set; }

        public Subject subject { get; set; }
        public Student student { get; set; }

    }
}
