using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Contracts
{
    public interface IGenericRepository<T> where T : SoftRemovableEntity
    {
        Task<T> GetAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(T entity);
        Task<bool> Exists(Guid id);
    }
}
