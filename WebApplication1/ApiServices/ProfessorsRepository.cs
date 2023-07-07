
using ApiDtoLibrary.Professors;
using AutoMapper;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using UniversityApi.API.Contracts;
using WebApplication1.Queries;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices
{
    public class ProfessorsRepository : GenericRepository<Professor, BaseProfessor>, IProfessorsRepository
    {
        public ProfessorsRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += "/Professors";
        }

        protected override string GetPathForGetByKey(string key) => $"{_apiPath}/idCode/{key}";

        public List<Professor> SortFilterProfessors(ProfessorOrderByOptions orderByOption, ProfessorFilterByOptions filterByOption, string filter)
        {
            throw new System.NotImplementedException();
        }  
    }
}

