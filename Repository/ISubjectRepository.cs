using System.Collections.Generic;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public interface ISubjectRepository
    {
        Task<Subject> AddSubjectAsync(Subject Subject);
        Task<Subject> DeleteSubjectAsync(string id);
        Task<Subject> FindSubjectAsync(string id);
        Task<List<Subject>> GetAllSubjectsAsync();
        Task<Subject> GetSubjectAsync(string Id);
        string getSubjectId(string name);
        string getSubjectName(string id);
        int getSubject_CreditHours(string id);
        Task<Subject> UpdateSubjectAsync(Subject Subject);
    }
}