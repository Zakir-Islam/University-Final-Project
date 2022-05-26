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
                
                if (item.Marks >= 85)
                {
                    item.gpa = 4.00;
                    item.Grade = "A+";
                }
                else if(item.Marks>=80 && item.Marks < 85)
                {
                    item.gpa = 3.7;
                    item.Grade = "A-";
                }
                else if (item.Marks >= 75 && item.Marks < 80)
                {
                    item.gpa = 3.3;
                    item.Grade = "B+";
                }
                else if (item.Marks >= 70 && item.Marks < 75)
                {
                    item.gpa = 3.0;
                    item.Grade = "B";
                }
          
                else if (item.Marks >= 65 && item.Marks < 70)
                {
                    item.gpa = 2.7;
                    item.Grade = "B-";
                }
                else if (item.Marks >= 60 && item.Marks < 65)
                {
                    item.gpa = 2.3;
                    item.Grade = "C+";
                }
                else if (item.Marks >= 58 && item.Marks < 60)
                {
                    item.gpa = 2.0;
                    item.Grade = "C";
                }
                else if (item.Marks >= 55 && item.Marks < 58)
                {
                    item.gpa = 1.7;
                    item.Grade = "C-";
                }
                else if (item.Marks >= 50 && item.Marks < 55)
                {
                    item.gpa = 1.0;
                    item.Grade = "D";
                }
                else if (item.Marks < 50)
                {
                    item.gpa = 0.0;
                    item.Grade = "F";
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

        public int TotalCrHrs(List<ExamResult> examResults)
        {
            

            int totalCredit = 0;

           
            foreach (var item in examResults)
            {
                totalCredit = totalCredit + subjectRepository.getSubject_CreditHours(item.subject_FId);
            }

            

            return totalCredit;
        }

    }
}
