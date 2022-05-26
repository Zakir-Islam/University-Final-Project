using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public interface IDepartmentRepository
    {
        Task<Department> AddDepartmentAsync(Department department);
        Task<Department> DeleteDepartmentAsync(string id);
        Task<Department> UpdateDepartmentAsync(Department department);
        Task<List<Department>> GetAllDepartmentAsync();
        Task<Department> GetDepartmentAsync(string id);
    }
}
