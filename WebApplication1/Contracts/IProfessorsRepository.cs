using ApiDtoLibrary.Professors;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Extensions;
using WebApplication1.Services.People;

namespace UniversityApi.API.Contracts
{
    public interface IProfessorsRepository : IGenericRepository<Professor, GetProfessor>
    {
        List<Professor> SortFilterProfessors(ProfessorOrderByOptions orderByOption, ProfessorFilterByOptions filterByOption, string filter);

        Task<bool> IdCodeIsOccupied(string idCode);
    }
}
