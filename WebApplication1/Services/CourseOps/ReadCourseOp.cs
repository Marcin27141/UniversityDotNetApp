using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.Queries;
using WebApplication1.Services.People;

namespace WebApplication1.Services.CourseOps
{
    public class ReadCourseOp : IReadCourseOp
    {
        private readonly AppDbContext _context;
        public ReadCourseOp(AppDbContext context) => _context = context;

        public Course GetCourseByCode(string courseCode)
        {
            var course = _context.Courses.AsNoTracking()
                .Include(c => c.Professor)
                    .ThenInclude(p => p.PersonalData)
                .Include(c => c.Students)
                    .ThenInclude(sc => sc.Student)
                        .ThenInclude(s => s.PersonalData)
                .SingleOrDefault(c => c.CourseCode.Equals(courseCode));
            if (course == null) return null;
            return Course.FromEntityCourse(course);
        }

        public List<Course> GetAllCourses()
        {
            return _context.Courses
                .AsNoTracking()
                .Include(c => c.Professor)
                    .ThenInclude(p => p.PersonalData)
                .Include(c => c.Students)
                    .ThenInclude(sc => sc.Student)
                        .ThenInclude(s => s.PersonalData)
                .Select(c => Course.FromEntityCourse(c))
                .ToList();
        }

        public List<Course> SortFilterCourses(CourseOrderByOptions orderByOption, CourseFilterByOptions filterByOption, string filter)
        {
            return _context.Courses
                .AsNoTracking()
                .Include(c => c.Professor)
                    .ThenInclude(p => p.PersonalData)
                .Include(c => c.Students)
                    .ThenInclude(sc => sc.Student)
                        .ThenInclude(s => s.PersonalData)
                .OrderCoursesBy(orderByOption)
                .FilterCoursesBy(filterByOption, filter)
                .MapEntitiesToCourses()
                .ToList();
        }
    }

    public interface IReadCourseOp
    {
        Course GetCourseByCode(string courseCode);
        List<Course> GetAllCourses();
        List<Course> SortFilterCourses(CourseOrderByOptions orderByOption, CourseFilterByOptions filterByOption, string filter);
    }
}
