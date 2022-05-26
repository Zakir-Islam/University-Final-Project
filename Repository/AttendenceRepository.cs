using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_Final_Project.Models;

namespace University_Final_Project.Repository
{
    public class AttendenceRepository : IAttendenceRepository
    {
        private readonly ExamContext examContext;

        public AttendenceRepository(ExamContext examContext)
        {
            this.examContext = examContext;
        }



        public async Task<Attendence> AddAttendenceAsync(Attendence Attendence)
        {
            await examContext.attendences.AddAsync(Attendence);

            await examContext.SaveChangesAsync();
            return Attendence;

        }

        public async Task<List<Attendence>> GetAllAttendencesAsync()
        {
            return await examContext.attendences.ToListAsync();
        }

        public async Task<Attendence> GetAttendenceAsync(int Id)
        {
            var Attendence = await examContext.attendences.FindAsync(Id);

            return Attendence;

        }

        public async Task<Attendence> DeleteAttendenceAsync(int id)
        {
            var Attendence = examContext.attendences.Find(id);
            examContext.attendences.Remove(Attendence);
            await examContext.SaveChangesAsync();
            return Attendence;
        }

        public async Task<Attendence> UpdateAttendenceAsync(Attendence Attendence)
        {
            examContext.attendences.Update(Attendence);
            await examContext.SaveChangesAsync();

            return Attendence;
        }


    }
}
