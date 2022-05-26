using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class TranscriptWithStudent
    {
        public Student student { get; set; }
        public  List<Transcript> transcripts { get; set; }
    }
}
