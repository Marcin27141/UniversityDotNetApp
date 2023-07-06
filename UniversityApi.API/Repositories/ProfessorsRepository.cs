using Microsoft.EntityFrameworkCore;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Repositories
{
    public class ProfessorsRepository : GenericRepository<EntityProfessor>, IProfessorsRepository
    {
        private readonly UniversityApiDbContext _context;

        public ProfessorsRepository(UniversityApiDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
