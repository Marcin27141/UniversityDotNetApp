using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using AutoMapper;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Professors
{
    public class ProfessorPostRepository : GenericPostRepository<Professor, PostProfessor, GetProfessor>, IGenericPostRepository<Professor, GetProfessor>
    {
        public ProfessorPostRepository(IMapper mapper, IAuthenticationRepository authenticationRepository) : base(mapper, authenticationRepository)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Professor);
        }

        public override async Task<GetProfessor> AddAsync(Professor entity)
        {
            var response = await base.AddAsync(entity);

            var entityPersonIdClaim = new Claim("EntityPersonId", response.EntityPersonID.ToString());
            await base.AddClaimAfterPostAsync(response.EntityPersonID.ToString(), entityPersonIdClaim);

            return response;
        }
    }
}
