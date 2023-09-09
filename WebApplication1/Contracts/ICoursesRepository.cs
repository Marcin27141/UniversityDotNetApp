using ApiDtoLibrary.Courses;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Extensions;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface ICoursesRepository : IGenericRepository<Course, GetCourse>
    {
        List<Course> SortFilterCourses(CourseOrderByOptions orderByOption, CourseFilterByOptions filterByOption, string filter);
        Task<bool> CourseCodeIsOccupied(string courseCode);
    }
}
