using ApiDtoLibrary.Students;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
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

        public override async Task<EntityStudent> GetByUserAsync(string id)
        {
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

        public async Task<string> UpdateWithCoursesAsync(EntityStudent updatedStudent, IEnumerable<string> coursesCodes)
        {
            var updatedCourses = coursesCodes
                .Select(c => _context.Courses.SingleOrDefault(o => o.CourseCode.Equals(c)))
                .ToList();
            var studentToUpdate = _context.Set<EntityStudent>()
                .Include(s => s.Courses)
                .SingleOrDefault(s => s.Index.Equals(updatedStudent.Index));
            studentToUpdate.Courses = updatedCourses;
            _context.Update(studentToUpdate);
            await _context.SaveChangesAsync();
            return studentToUpdate.Index;
        }

        public async Task DeleteStudentsCourseAsync(int id, string courseCode)
        {
            var student = await GetAsync(id);
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseCode.Equals(courseCode));
            student.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
