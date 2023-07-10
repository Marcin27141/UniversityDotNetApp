using ApiDtoLibrary;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Contracts
{
    public interface IPeopleRespository
    {
        Task<EntityPerson> GetAsync(Guid id);
        Task<List<EntityPerson>> GetAllPersonalDataAsync();
        Task DeleteAsync(Guid id);
    }
}
