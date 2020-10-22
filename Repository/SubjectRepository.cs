using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class SubjectRepository
    {
        private readonly ExamContext examContext;

        public SubjectRepository(ExamContext examContext)
        {
            this.examContext = examContext;
        }

        public string getSubjectName(int id)
        {
            var subject = this.examContext.Subjects.Find(id);
            return (subject.corse_title);        
        }

        public int getSubject_CreditHours(int id)
        {
            var subject = this.examContext.Subjects.Find(id);
            return (subject.credit_hours);
        }
    }
}
