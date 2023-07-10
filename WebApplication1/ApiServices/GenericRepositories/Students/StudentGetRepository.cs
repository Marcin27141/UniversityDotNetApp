using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using AutoMapper;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Students
{
    public class StudentGetRepository : GenericGetRepository<Student, GetStudent>, IGenericGetRepository<Student>
    {
        public StudentGetRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}
