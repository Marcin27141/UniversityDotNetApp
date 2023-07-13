using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using AutoMapper;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Professors
{
    public class ProfessorPostRepository : GenericPostRepository<Professor, PostProfessor>, IGenericPostRepository<Professor>
    {
        public ProfessorPostRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Professor);
        }
    }
}
