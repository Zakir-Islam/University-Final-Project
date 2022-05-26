using System.Collections.Generic;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public interface IStudentRepository
    {
        Task<Student> AddStudentAsync(Student student);
        Task<Student> DeleteStudentAsync(string id);
        Task<Student> FindStudentAsync(string id);
        Task<List<Student>> GetAllStudentsAsync();
        Task<string> GetFullNameAsync(string id);
        Task<Student> GetStudentAsync(string Id);
        Task<Student> UpdateStudentAsync(Student student);
    }
}