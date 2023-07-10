using ApiDtoLibrary.Courses;
using AutoMapper;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.ApiServices.GenericRepositories.Courses
{
    public class CourseGetRepository : GenericGetRepository<Course, GetCourse>, IGenericGetRepository<Course>
    {
        public CourseGetRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}
