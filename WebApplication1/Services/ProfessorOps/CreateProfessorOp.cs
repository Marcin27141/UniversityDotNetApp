using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.People;

namespace WebApplication1.Services.ProfessorOps
{
    public class CreateProfessorOp : ICreateProfessorOp
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateProfessorOp(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<string> AddProfessorAsync(Professor professor)
        {
            var entityProfessor = professor.ToEntityProfessor();
            var professorWithSameIdCode = _context.Professors.IgnoreQueryFilters()
                .Include(p => p.PersonalData)
                .SingleOrDefault(p => p.IdCode.Equals(entityProfessor.IdCode));

            if (professorWithSameIdCode != null && !professorWithSameIdCode.SoftDeleted)
                throw new Exception("Professor with given id code is already added");

            using (var transaction = _context.Database.BeginTransaction())
            {
                if (professorWithSameIdCode != null)
                {
                    _context.Remove(professorWithSameIdCode.PersonalData);
                    _context.SaveChanges();
                }

                _context.Add(entityProfessor);
                _context.SaveChanges();

                transaction.Commit();
            }

            var entityIdClaim = new Claim("EntityId", _context.Professors.SingleOrDefault(p => p.IdCode.Equals(professor.IdCode)).EntityProfessorID.ToString());
            _userManager.AddClaimAsync(professor.PersonalData.ApplicationUser, entityIdClaim);

            var idCodeClaim = new Claim("IdCode", professor.IdCode);
            _userManager.AddClaimAsync(professor.PersonalData.ApplicationUser, idCodeClaim);

            return entityProfessor.IdCode;
        }
    }

    public interface ICreateProfessorOp
    {
        Task<string> AddProfessorAsync(Professor professor);
    }
}
