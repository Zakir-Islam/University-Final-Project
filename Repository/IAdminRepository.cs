using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public interface IAdminRepository
    {
        RoleManager<IdentityRole> RoleManager { get; }

        Task<Admin> AddAdminAsync(Admin Admin);
        Task<Admin> DeleteAdminAsync(string id);
        Task<Admin> FindAdminAsync(string id);
        Task<Admin> GetAdminAsync(string Id);
        Task<List<Admin>> GetAllAdminsAsync();
        Task<Admin> UpdateAdminAsync(Admin Admin);
    }
}