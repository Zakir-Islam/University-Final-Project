using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class DegreeRepository : IDegreeRepository
    {
        private readonly ExamContext context;

        public DegreeRepository(ExamContext context)
        {
            this.context = context;
        }

        public async Task<Degree> AddDegreeAsync(Degree Degree)
        {
            await context.Degrees.AddAsync(Degree);
            await context.SaveChangesAsync();
            return Degree;
        }

        public async Task<Degree> DeleteDegreeAsync(string id)
        {
            if (id != null)
            {
                var Degree = await context.Degrees.FindAsync(id);
                context.Degrees.Remove(Degree);
                await context.SaveChangesAsync();
                return Degree;
            }
            return null;
        }

        public async Task<List<Degree>> GetAllDegreeAsync()
        {
            return await context.Degrees.ToListAsync();
        }

        public async Task<Degree> GetDegreeAsync(string id)
        {
            return await context.Degrees.FindAsync(id);
        }

        public async Task<Degree> UpdateDegreeAsync(Degree Degree)
        {

            context.Degrees.Update(Degree);
            await context.SaveChangesAsync();

            return Degree;

        }
    }
}
