using ApiDtoLibrary.Courses;
using AutoMapper;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.ApiServices.GenericRepositories.Courses
{
    public class CoursePostRepository : GenericPostRepository<Course, PostCourse>, IGenericPostRepository<Course>
    {
        public CoursePostRepository(IMapper mapper) : base(mapper)
        {

        }

        protected override string GetPathForCreate() => $"{_apiPath}/Courses";
    }
}
