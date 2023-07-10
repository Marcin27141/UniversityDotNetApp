using AutoMapper;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.ApiServices.GenericRepositories.Courses
{
    public class CourseRepository : GenericRepository<Course>, IGenericRepository<Course>
    {
        public CourseRepository(IMapper mapper, IGenericGetRepository<Course> getRepository, IGenericPostRepository<Course> postRepository, IGenericPutRepository<Course> putRepository) : base(mapper, getRepository, postRepository, putRepository)
        {
        }

        protected override string GetPathForDelete(object entityId) => $"{_apiPath}/Courses/{entityId}";
    }
}
