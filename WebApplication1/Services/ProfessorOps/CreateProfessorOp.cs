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

        public async Task<string> AddProfessorAsync(People.Professor professor)
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

            var entityIdClaim = new Claim("EntityId", _context.Professors.SingleOrDefault(p => p.IdCode.Equals(professor.IdCode)).ProfessorID.ToString());
            _userManager.AddClaimAsync(professor.PersonalData.ApplicationUser, entityIdClaim);

            var idCodeClaim = new Claim("IdCode", professor.IdCode);
            _userManager.AddClaimAsync(professor.PersonalData.ApplicationUser, idCodeClaim);

            return entityProfessor.IdCode;
        }
    }

    public interface ICreateProfessorOp
    {
        Task<string> AddProfessorAsync(People.Professor professor);
    }
}
