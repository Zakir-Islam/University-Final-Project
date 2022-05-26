using System.Collections.Generic;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public interface IAttendenceRepository
    {
        Task<Attendence> AddAttendenceAsync(Attendence Attendence);
        Task<Attendence> DeleteAttendenceAsync(int id);
        Task<List<Attendence>> GetAllAttendencesAsync();
        Task<Attendence> GetAttendenceAsync(int Id);
        Task<Attendence> UpdateAttendenceAsync(Attendence Attendence);
    }
}