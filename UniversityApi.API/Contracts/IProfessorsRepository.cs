using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Contracts
{
    public interface IProfessorsRepository : IGenericRepository<EntityProfessor>
    {
        Task<EntityProfessor> GetByIdCodeAsync(string idCode);
    }
}
