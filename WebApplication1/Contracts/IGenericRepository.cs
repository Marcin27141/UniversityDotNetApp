using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Queries;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Contracts
{
    public interface IGenericRepository<T> where T : IDistinguishableEntity
    {
        T GetById(int id);
        List<T> GetAll();
        T GetByUser(string userId);
        Task<string> AddAsync(T entity);
        Task<string> UpdateAsync(int id, T updatedEntity);
        Task DeleteByIdAsync(int id);
    }
}
