using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ExamContext context;

        public DepartmentRepository(ExamContext context)
        {
            this.context = context;
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> DeleteDepartmentAsync(string id)
        {
            if (id != null)
            {
                var department = await context.Departments.FindAsync(id);
                context.Departments.Remove(department);
                await context.SaveChangesAsync();
                return department;
            }
            return null;
        }

        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            return await context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentAsync(string id)
        {
            return await context.Departments.FindAsync(id);
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            
                context.Departments.Update(department);
               await context.SaveChangesAsync();

            return department;
            
        }
    }
}
