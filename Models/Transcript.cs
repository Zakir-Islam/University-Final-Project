using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class Transcript
    {
        public int semester { get; set; }

        public float GPA { get; set; }

        public float CGPA { get; set; }

        public int CrHrs { get; set; }
        public List<ExamResult> examResults { get; set; }
    }
}
