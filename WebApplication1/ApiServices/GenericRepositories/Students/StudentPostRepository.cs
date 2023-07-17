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
        private readonly IAuthenticationRepository _authenticationRepository;

        public StudentPostRepository(IMapper mapper, IAuthenticationRepository authenticationRepository) : base(mapper)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Student);
            _authenticationRepository = authenticationRepository;
        }

        public override async Task<GetStudent> AddAsync(Student entity)
        {
            var response = await base.AddAsync(entity);

            var entityPersonIdClaim = new Claim("EntityPersonId", response.EntityPersonID.ToString());
            await _authenticationRepository.AddClaimAsync(response.ApplicationUserId, entityPersonIdClaim);

            return response;
        }
    }
}
