using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface IGenericRepository<T> : IGenericGetRepository<T>, IGenericPostRepository<T>, IGenericPutRepository<T>
        where T : IDistinguishableEntity
    {
        Task DeleteAsync(Guid id);
    }
}
