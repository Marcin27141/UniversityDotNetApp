using ApiDtoLibrary;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Contracts
{
    public interface IPeopleRespository
    {
        Task<List<EntityPerson>> GetAllPersonalDataAsync();
    }
}
