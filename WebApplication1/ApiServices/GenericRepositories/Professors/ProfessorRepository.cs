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
    public class ProfessorRepository : GenericRepository<Professor>, IProfessorsRepository
    {
        public ProfessorRepository(IMapper mapper, IGenericGetRepository<Professor> getRepository, IGenericPostRepository<Professor> postRepository, IGenericPutRepository<Professor> putRepository) : base(mapper, getRepository, postRepository, putRepository)
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
    }
}
