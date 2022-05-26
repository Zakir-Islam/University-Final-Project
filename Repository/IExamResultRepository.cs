using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public interface IExamResultRepository
    {
        RoleManager<IdentityRole> RoleManager { get; }

        Task<ExamResult> AddExamResultAsync(ExamResult ExamResult, IFormFile file, int semester);
        Task<ExamResult> DeleteExamResultAsync(int id);
        Task<ExamResult> FindExamResultAsync(int id);
        Task<List<ExamResult>> GetAllExamResultsAsync(string searchString);
        Task<ExamResult> GetExamResultAsync(int Id);
        Task<ExamResult> UpdateExamResultAsync(ExamResult ExamResult, int semester);
    }
}