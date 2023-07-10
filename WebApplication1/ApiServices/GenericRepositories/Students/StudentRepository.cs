using AutoMapper;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Students
{
    public class StudentRepository : GenericRepository<Student>, IGenericRepository<Student>
    {
        public StudentRepository(IMapper mapper, IGenericGetRepository<Student> getRepository, IGenericPostRepository<Student> postRepository, IGenericPutRepository<Student> putRepository) : base(mapper, getRepository, postRepository, putRepository)
        {
        }

        protected override string GetPathForDelete(object entityId) => $"{_apiPath}/Students/{entityId}";
    }
}
