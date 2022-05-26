using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public interface IDegreeRepository
    {
        Task<Degree> AddDegreeAsync(Degree degree);
        Task<Degree> DeleteDegreeAsync(string id);
        Task<Degree> UpdateDegreeAsync(Degree degree);
        Task<List<Degree>> GetAllDegreeAsync();
        Task<Degree> GetDegreeAsync(string id);
    }
}
