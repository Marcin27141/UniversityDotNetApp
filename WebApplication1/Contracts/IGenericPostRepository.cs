using System;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface IGenericPostRepository<T> where T : IDistinguishableEntity
    {
        Task<Guid> AddAsync(T entity);
    }
}
