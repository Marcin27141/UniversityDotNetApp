using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;

namespace WebApplication1.Services.ProfessorOps
{
    public class DeleteProfessorOp : IDeleteProfessorOp
    {
        private readonly AppDbContext _context;
        public DeleteProfessorOp(AppDbContext context) => _context = context;

        public async Task DeleteProfessorByIdCodeAsync(string idCode)
        {
            var entityProfessor = _context.Professors.Include(p => p.PersonalData).SingleOrDefault(p => p.IdCode.Equals(idCode));
            if (entityProfessor == null) throw new Exception("Professor doesn't exist");
            entityProfessor.SoftDeleted = true;
            entityProfessor.PersonalData.SoftDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public interface IDeleteProfessorOp
    {
        Task DeleteProfessorByIdCodeAsync(string idCode);
    }
}
