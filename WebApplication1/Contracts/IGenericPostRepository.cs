using System;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface IGenericPostRepository<T, U> where T : IDistinguishableEntity
    {
        Task<U> AddAsync(T entity);
    }
}
