using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Queries;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Contracts
{
    public interface IGenericPutRepository<T> where T : IDistinguishableEntity
    {
        Task<Guid> UpdateAsync(T updatedEntity);
    }
}
