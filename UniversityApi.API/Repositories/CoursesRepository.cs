using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.Repositories
{
    public class CoursesRepository : GenericRepository<EntityCourse>, ICoursesRepository
    {
        public CoursesRepository(UniversityApiDbContext dbContext, UserManager<ApiUser> userManager) : base(dbContext, userManager)
        {
        }

        public override async Task<EntityCourse> GetAsync(Guid id)
        {
            return await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.Students)
                .SingleOrDefaultAsync(c => c.EntityCourseID == id);
        }

        public override Task<EntityCourse> GetByUserAsync(string id)
        {
            throw new System.InvalidOperationException();
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
