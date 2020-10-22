using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class GradeCalculator
    {
        private readonly SubjectRepository subjectRepository;

        public GradeCalculator(SubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }
        //gpa 
        //--------------------------------------------
        public List<ExamResult> calculateGpa(List<ExamResult> examResults) { 
        
            foreach(var item in examResults)
            {
                var marks = item.mid + item.final + item.sessional;
                if (marks >= 85)
                {
                    item.gpa = 4.00;
                }
                else if(marks>=80 && marks < 85)
                {
                    item.gpa = 3.7;
                }
                else if (marks >= 75 && marks < 80)
                {
                    item.gpa = 3.3;
                }
                else if (marks >= 70 && marks < 75)
                {
                    item.gpa = 3.0;
                }

            }

            return examResults;
        }
        //cgpa

        //------------------------------------------
        public double calculateCgpa(List<ExamResult> examResults)
        {
            examResults = calculateGpa(examResults);

            int totalCredit=0;

            double cgpa=0.0;

            double sum=0.0;
            foreach(var item in examResults)
            {
                totalCredit+= subjectRepository.getSubject_CreditHours(item.subject_FId);
                sum += item.gpa * subjectRepository.getSubject_CreditHours(item.subject_FId);
            }

            cgpa = sum / totalCredit;
            
            return cgpa;
        }

     }
}
