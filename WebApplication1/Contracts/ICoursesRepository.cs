using System.Collections.Generic;
using WebApplication1.Contracts;
using WebApplication1.Queries;
using WebApplication1.Services;

namespace UniversityApi.API.Contracts
{
    public interface ICoursesRepository : IGenericRepository<Course>
    {
        List<Course> SortFilterCourses(CourseOrderByOptions orderByOption, CourseFilterByOptions filterByOption, string filter);
    }
}
