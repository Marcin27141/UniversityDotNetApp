using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Repositories
{
    public class ProfessorsRepository : GenericRepository<EntityProfessor>, IProfessorsRepository
    {
        public ProfessorsRepository(UniversityApiDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IdCodeIsOccupied(string idCode)
        {
            return await _context.Set<EntityProfessor>().AnyAsync(s => s.IdCode.Equals(idCode));
        }
    }
}

