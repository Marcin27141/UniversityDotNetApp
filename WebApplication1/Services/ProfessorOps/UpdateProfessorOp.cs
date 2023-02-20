using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.People;

namespace WebApplication1.Services.ProfessorOps
{
    public class UpdateProfessorOp : IUpdateProfessorOp
    {
        private readonly AppDbContext _context;
        public EntityProfessor ProfessorToUpdate { get; private set; }

        public UpdateProfessorOp(AppDbContext context) => _context = context;

        public Professor GetProfessorToUpdateByIdCode(string idCode)
        {
            ProfessorToUpdate = _context.Professors
                .Include(p => p.PersonalData)
                .ThenInclude(pd => pd.ApplicationUser)
                .SingleOrDefault(p => p.IdCode.Equals(idCode));
            if (ProfessorToUpdate == null) return null;
            return Professor.FromEntityProfessor(ProfessorToUpdate);
        }

        public async Task<string> UpdateProfessorAsync(Professor updatedProfessor)
        {
            var updatedEntity = updatedProfessor.ToEntityProfessor();
            ProfessorToUpdate = _context.Professors.Include(p => p.PersonalData).SingleOrDefault(p => p.IdCode.Equals(updatedProfessor.IdCode));
            UpdateProfessor(updatedEntity);
            await _context.SaveChangesAsync();
            return ProfessorToUpdate.IdCode;
        }

        private void UpdateProfessor(EntityProfessor updatedProfessor)
        {
            ProfessorToUpdate.PersonalData.FirstName = updatedProfessor.PersonalData.FirstName;
            ProfessorToUpdate.PersonalData.LastName = updatedProfessor.PersonalData.LastName;
            ProfessorToUpdate.PersonalData.PESEL = updatedProfessor.PersonalData.PESEL;
            ProfessorToUpdate.PersonalData.Birthday = updatedProfessor.PersonalData.Birthday;
            ProfessorToUpdate.PersonalData.Motherland = updatedProfessor.PersonalData.Motherland;

            ProfessorToUpdate.Subject = updatedProfessor.Subject;
            ProfessorToUpdate.FirstDayAtJob = updatedProfessor.FirstDayAtJob;
            ProfessorToUpdate.Salary = updatedProfessor.Salary;
        }
    }

    public interface IUpdateProfessorOp
    {
        Professor GetProfessorToUpdateByIdCode(string idCode);
        Task<string> UpdateProfessorAsync(Professor updatedProfessor);
    }
}
