using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class Subject
    {
        [Key]
        public int Subject_Id { get; set; }

        public String corse_title { get; set; }

        public int credit_hours { get; set; }

        public ICollection<ExamResult> examResults{get;set;}

    }
}
