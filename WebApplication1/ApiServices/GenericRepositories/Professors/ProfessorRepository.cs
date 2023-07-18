using ApiDtoLibrary.Professors;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApi.API.Contracts;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Extensions;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Professors
{
    public class ProfessorRepository : GenericRepository<Professor, GetProfessor>, IProfessorsRepository
    {
        public ProfessorRepository(
            IMapper mapper,
            IAuthenticationRepository authenticationRepository,
            IGenericGetRepository<Professor> getRepository,
            IGenericPostRepository<Professor, GetProfessor> postRepository,
            IGenericPutRepository<Professor> putRepository
            ) : base(mapper, authenticationRepository, getRepository, postRepository, putRepository)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Professor);
        }

        public async Task<bool> IdCodeIsOccupied(string idCode)
        {
            string checkIdCodePath = $"{_apiPath}/IdCode";
            var response = await _httpClient.GetAsync(checkIdCodePath);
            if (response.IsSuccessStatusCode)
            {
                return bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return default;
        }

        public List<Professor> SortFilterProfessors(ProfessorOrderByOptions orderByOption, ProfessorFilterByOptions filterByOption, string filter)
        {
            var allProfessors = GetAllAsync().Result;
            return allProfessors
                .OrderProfessorsBy(orderByOption)
                .FilterProfessorsBy(filterByOption, filter)
                .ToList();
        }

        public override async Task DeleteAsync(Professor entity)
        {
            await base.DeleteAsync(entity);
            await RemoveClaimAfterDeleteAsync(entity.ApplicationUserId, "EntityPersonId");
        }
    }
}
