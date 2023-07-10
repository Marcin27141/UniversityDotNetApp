using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using AutoMapper;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Students
{
    public class StudentPostRepository : GenericPostRepository<Student, PostStudent>, IGenericPostRepository<Student>
    {
        public StudentPostRepository(IMapper mapper) : base(mapper)
        {

        }

        protected override string GetPathForCreate() => $"{_apiPath}/Students";
    }
}
