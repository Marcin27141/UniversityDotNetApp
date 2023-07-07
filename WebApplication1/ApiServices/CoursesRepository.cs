
using ApiDtoLibrary.Professors;
using AutoMapper;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using UniversityApi.API.Contracts;
using WebApplication1.Queries;
using WebApplication1.Services.People;
using ApiDtoLibrary.Courses;
using WebApplication1.Services;
using ApiDtoLibrary;

namespace WebApplication1.ApiServices
{
    public class CoursesRepository : GenericRepository<Course, BaseCourse>, ICoursesRepository
    {
        public CoursesRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += "/Courses";
        }

        public override Course GetByUser(string userId)
        {
            throw new System.InvalidOperationException();
        }

        public List<Course> SortFilterCourses(CourseOrderByOptions orderByOption, CourseFilterByOptions filterByOption, string filter)
        {
            throw new System.NotImplementedException();
        }
    }
}

