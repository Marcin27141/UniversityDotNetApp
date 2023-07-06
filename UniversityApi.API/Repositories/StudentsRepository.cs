using Microsoft.EntityFrameworkCore;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Repositories
{
    public class StudentsRepository : GenericRepository<EntityStudent>, IStudentsRepository
    {
        private readonly UniversityApiDbContext _context;

        public StudentsRepository(UniversityApiDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public override async Task<EntityStudent> GetAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }
            return await _context.Set<EntityStudent>()
                .Include(s => s.Courses)
                    .ThenInclude(c => c.Professor)
                .SingleOrDefaultAsync(s => s.EntityPersonID == id);
        }

        public override async Task<List<EntityStudent>> GetAllAsync()
        {
            return await _context.Set<EntityStudent>()
                .Include(s => s.Courses)
                    .ThenInclude(c => c.Professor)
                .ToListAsync();
        }
    }
}
