using Microsoft.EntityFrameworkCore;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Repositories
{
    public class CoursesRepository : GenericRepository<EntityCourse>, ICoursesRepository
    {
        private readonly UniversityApiDbContext _context;

        public CoursesRepository(UniversityApiDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<EntityCourse> GetAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }
            return await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.Students)
                .SingleOrDefaultAsync(c => c.EntityCourseID == id);
        }

        public override async Task<List<EntityCourse>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.Students)
                .ToListAsync();
        }
    }
}
