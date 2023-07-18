using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface IGenericRepository<T, U> : IGenericGetRepository<T>, IGenericPostRepository<T, U>, IGenericPutRepository<T>
        where T : IDistinguishableEntity
    {
        Task DeleteAsync(T entity);
    }
}
