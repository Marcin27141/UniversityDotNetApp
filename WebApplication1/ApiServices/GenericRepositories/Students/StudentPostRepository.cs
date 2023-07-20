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

namespace WebApplication1.ApiServices.GenericRepositories.Students
{
    public class StudentPostRepository : GenericPostRepository<Student, PostStudent, GetStudent>, IGenericPostRepository<Student, GetStudent>
    {
        public StudentPostRepository(IMapper mapper, IAuthenticationRepository authenticationRepository) : base(mapper, authenticationRepository)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Student);
        }

        public override async Task<GetStudent> AddAsync(Student entity)
        {
            var response = await base.AddAsync(entity);

            var entityPersonIdClaim = new Claim("EntityPersonId", response.EntityPersonId.ToString());
            await base.AddClaimAfterPostAsync(response.EntityPersonId.ToString(), entityPersonIdClaim);

            return response;
        }
    }
}
