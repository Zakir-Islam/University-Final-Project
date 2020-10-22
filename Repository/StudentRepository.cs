using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class StudentRepository
    {
        private readonly ExamContext examContext;

        public StudentRepository(ExamContext examContext)
        {
            this.examContext = examContext;
        }

        public string geStudentName(int id)
        {
            string name="N nameo";
           
                var student = this.examContext.Students.Find(id);
                if (student != null)
                {

                    name = student.FirstMidName + " " + student.LastName;
                }
            


            return name;
        }
    }
}
