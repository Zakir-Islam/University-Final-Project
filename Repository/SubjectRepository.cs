using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ExamContext examContext;

        public SubjectRepository(ExamContext examContext)
        {
            this.examContext = examContext;
        }

        public string getSubjectName(string id)
        {
            if (id != null)
            {
                var subject = this.examContext.Subjects.Find(id);
                if (subject != null)
                {
                    return (subject.corse_title);
                }
            }
          
            return "No name";
        }
        public string getSubjectId(string name)
        {
            var subject = this.examContext.Subjects.Where(s => s.corse_title == name).FirstOrDefault();
            if (subject != null)
            {
                return (subject.Subject_Id);
            }
            else
            {
                return null;
            }

        }
        public int getSubject_CreditHours(string id)
        {
            var subject = this.examContext.Subjects.Find(id);
            return (subject.credit_hours);
        }

        public async Task<Subject> AddSubjectAsync(Subject Subject)
        {
            await examContext.Subjects.AddAsync(Subject);

            await examContext.SaveChangesAsync();
            return Subject;

        }

        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            return await examContext.Subjects.ToListAsync();
        }

        public async Task<Subject> GetSubjectAsync(string Id)
        {
            var Subject = await examContext.Subjects.FindAsync(Id);

            return Subject;

        }

        public async Task<Subject> DeleteSubjectAsync(string id)
        {
            var Subject = examContext.Subjects.Find(id);
            examContext.Subjects.Remove(Subject);
            await examContext.SaveChangesAsync();
            return Subject;
        }

        public async Task<Subject> UpdateSubjectAsync(Subject Subject)
        {
            examContext.Subjects.Update(Subject);
            await examContext.SaveChangesAsync();

            return Subject;
        }

        public async Task<Subject> FindSubjectAsync(string id)
        {
            var Subject = await examContext.Subjects.FindAsync(id);


            return Subject;
        }
    }
}
