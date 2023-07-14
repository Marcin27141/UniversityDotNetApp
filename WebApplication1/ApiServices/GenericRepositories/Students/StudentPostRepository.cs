using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using AutoMapper;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Students
{
    public class StudentPostRepository : GenericPostRepository<Student, PostStudent, GetStudent>, IGenericPostRepository<Student, GetStudent>
    {
        public StudentPostRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Student);
        }
    }
}
