using AutoMapper;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Professors
{
    public class ProfessorRepository : GenericRepository<Professor>, IGenericRepository<Professor>
    {
        public ProfessorRepository(IMapper mapper, IGenericGetRepository<Professor> getRepository, IGenericPostRepository<Professor> postRepository, IGenericPutRepository<Professor> putRepository) : base(mapper, getRepository, postRepository, putRepository)
        {
        }

        protected override string GetPathForDelete(object entityId) => $"{_apiPath}/Professors/{entityId}";
    }
}
