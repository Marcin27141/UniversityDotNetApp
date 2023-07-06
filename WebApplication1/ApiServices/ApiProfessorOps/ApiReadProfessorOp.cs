using ApiDtoLibrary.Professors;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using WebApplication1.DataBase;
using WebApplication1.Queries;
using WebApplication1.Services.People;
using WebApplication1.Services.ProfessorOps;

namespace WebApplication1.ApiServices.ApiProfessorOps
{
    public class ApiReadProfessorOp : IReadProfessorOp
    {
        private static readonly string _baseApi = "https://localhost:7228/api/Professors";
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ApiReadProfessorOp(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Services.People.Professor> GetAllProfessors()
        {
            var response = _httpClient.GetAsync(_baseApi).Result;
            if (response.IsSuccessStatusCode)
            {
                var getProfessors = response.Content.ReadFromJsonAsync<List<GetProfessor>>().Result;
                var result =  _mapper.Map<List<Services.People.Professor>>(getProfessors);
                return result;
            }
            return Enumerable.Empty<Services.People.Professor>().ToList();
        }

        public Services.People.Professor GetProfessorByIdCode(string idCode)
        {
            var professor = _context.Professors.AsNoTracking().Include(p => p.PersonalData).SingleOrDefault(p => p.IdCode.Equals(idCode));
            if (professor == null) return null;
            return Services.People.Professor.FromEntityProfessor(professor);
        }

        public Services.People.Professor GetProfessorByUser(string userId)
        {
            var professor = _context.Professors.AsNoTracking()
                .Include(p => p.PersonalData)
                .ThenInclude(pd => pd.ApplicationUser)
                .SingleOrDefault(p => p.PersonalData.ApplicationUser.Id.Equals(userId));
            if (professor == null) return null;
            return Services.People.Professor.FromEntityProfessor(professor);
        }

        public List<Services.People.Professor> SortFilterProfessors(ProfessorOrderByOptions orderByOption, ProfessorFilterByOptions filterByOption, string filter)
        {
            return _context.Professors
                .AsNoTracking()
                .Include(s => s.PersonalData)
                .OrderProfessorsBy(orderByOption)
                .FilterProfessorsBy(filterByOption, filter)
                .MapEntitiesToProfessors()
                .ToList();
        }
    }
}
