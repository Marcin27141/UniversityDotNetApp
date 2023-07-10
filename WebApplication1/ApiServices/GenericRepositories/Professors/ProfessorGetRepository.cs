using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using AutoMapper;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Professors
{
    public class ProfessorGetRepository : GenericGetRepository<Professor, GetProfessor>, IGenericGetRepository<Professor>
    {
        public ProfessorGetRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}
