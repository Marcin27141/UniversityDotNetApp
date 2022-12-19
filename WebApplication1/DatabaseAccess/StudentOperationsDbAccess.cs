using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.DataBase.Entities;

namespace WebApplication1.DatabaseAccess
{
    public interface IStudentOperationsDbAccess
    {
        List<Course> GetEntityCoursesByIds(IEnumerable<int> coursesIds);
    }
    public class StudentOperationsDbAccess : IStudentOperationsDbAccess
    {
        private readonly AppDbContext _context;
        public StudentOperationsDbAccess(AppDbContext dbcontext) => _context = dbcontext;

        public List<Course> GetEntityCoursesByIds(IEnumerable<int> coursesIds)
        {
            return _context.Courses.Where(c => coursesIds.Contains(c.CourseID)).ToList();
        }
    }
}
