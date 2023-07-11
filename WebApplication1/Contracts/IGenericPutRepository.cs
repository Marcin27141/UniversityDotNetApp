using System;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface IGenericPutRepository<T> where T : IDistinguishableEntity
    {
        Task<Guid> UpdateAsync(T updatedEntity);
    }
}
