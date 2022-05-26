using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public interface ITeacherRepository
    {
        RoleManager<IdentityRole> RoleManager { get; }

        Task<Teacher> AddTeacherAsync(Teacher Teacher);
        Task<Teacher> DeleteTeacherAsync(string id);
        Task<Teacher> FindTeacherAsync(string id);
        Task<List<Teacher>> GetAllTeachersAsync();
        Task<Teacher> GetTeacherAsync(string Id);
        Task<Teacher> UpdateTeacherAsync(Teacher Teacher);
    }
}