using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Repositories
{
    public class CoursesRepository : GenericRepository<EntityCourse>, ICoursesRepository
    {
        public CoursesRepository(UniversityApiDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<EntityCourse> GetAsync(Guid id)
        {
            return await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.Students)
                .SingleOrDefaultAsync(c => c.EntityCourseID == id);
        }

        public override async Task<EntityCourse> AddAsync(EntityCourse entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public override async Task<List<EntityCourse>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.Students)
                .ToListAsync();
        }

        public override async Task DeleteAsync(Guid id)
        {
            var course = await GetAsync(id);
            course.SoftDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CourseCodeIsOccupied(string courseCode)
        {
            return await _context.Set<EntityCourse>().AnyAsync(s => s.CourseCode.Equals(courseCode));
        }

        public async Task<EntityCourse> AddWithProfessorId(EntityCourse entity, Guid professorId)
        {
            entity.Professor = await _context.Set<EntityProfessor>().FindAsync(professorId);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EntityCourse> UpdateWithProfessorId(EntityCourse course, Guid professorId)
        {
            course.Professor = _context.Set<EntityProfessor>().Find(professorId);
            _context.Update(course);
            await _context.SaveChangesAsync();
            return course;
        }
    }
}
