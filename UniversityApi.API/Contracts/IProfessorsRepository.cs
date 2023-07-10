using ApiDtoLibrary.Professors;
using System.Net.Http;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Contracts
{
    public interface IProfessorsRepository : IGenericRepository<EntityProfessor>
    {
        Task<bool> IdCodeIsOccupied(string idCode);
    }
}
