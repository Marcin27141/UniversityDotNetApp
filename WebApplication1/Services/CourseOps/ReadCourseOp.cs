using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;

namespace WebApplication1.Services.CourseOps
{
    public class ReadCourseOp : IReadCourseOp
    {
        private readonly AppDbContext _context;
        public ReadCourseOp(AppDbContext context) => _context = context;

        /*public List<Course> GetCoursesListByEnum(IEnumerable<string> courses)
        {
            List<DataBase.Entities.Course> entityCourses = new();
            for (int i = 0; i < courses.Count(); i++)
                entityCourses.Add(_context.Courses.Include(c => c.Professor).Find(courses.ElementAt(i)));
            return entityCourses.Select(c => Course.FromEntityCourse(c)).ToList();
        }*/

        public Course GetCourseByCode(string courseCode)
        {
            var course = _context.Courses.AsNoTracking().Include(c => c.Professor).ThenInclude(p => p.PersonalData).SingleOrDefault(c => c.CourseCode.Equals(courseCode));
            if (course == null) return null;
            return Course.FromEntityCourse(course);
        }

        public List<Course> GetAllCourses()
        {
            return _context.Courses
                .AsNoTracking()
                .Include(c => c.Professor)
                .ThenInclude(p => p.PersonalData)
                .Select(c => Course.FromEntityCourse(c))
                .ToList();
        }
    }

    public interface IReadCourseOp
    {
        Course GetCourseByCode(string courseCode);
        List<Course> GetAllCourses();
    }
}
