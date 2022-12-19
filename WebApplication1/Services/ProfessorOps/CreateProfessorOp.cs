using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.Services.People;

namespace WebApplication1.Services.ProfessorOps
{
    public class CreateProfessorOp : ICreateProfessorOp
    {
        private readonly AppDbContext _context;
        public CreateProfessorOp(AppDbContext context) => _context = context;

        public async Task<string> AddProfessorAsync(Professor professor)
        {
            var entityProfessor = professor.ToEntityProfessor();
            var professorWithSameIdCode = _context.Professors.IgnoreQueryFilters().SingleOrDefault(p => p.IdCode.Equals(entityProfessor.IdCode));

            if (professorWithSameIdCode != null && !professorWithSameIdCode.SoftDeleted)
                throw new Exception("Professor with given id code is already added");

            else if (professorWithSameIdCode != null)       //TODO implement transaction?
            {
                _context.Remove(professorWithSameIdCode);
                await _context.SaveChangesAsync();
            }

            _context.Add(entityProfessor);
            await _context.SaveChangesAsync();
            return entityProfessor.IdCode;
        }
    }

    public interface ICreateProfessorOp
    {
        Task<string> AddProfessorAsync(Professor professor);
    }
}
