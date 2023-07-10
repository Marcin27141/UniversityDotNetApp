using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Queries;
using WebApplication1.Services.People;

namespace UniversityApi.API.Contracts
{
    public interface IProfessorsRepository : IGenericRepository<Professor>
    {
        List<Professor> SortFilterProfessors(ProfessorOrderByOptions orderByOption, ProfessorFilterByOptions filterByOption, string filter);

        Task<bool> IdCodeIsOccupied(string idCode);
    }
}
