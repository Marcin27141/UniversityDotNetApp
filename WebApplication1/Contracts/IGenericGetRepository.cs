using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Queries;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Contracts
{
    public interface IGenericGetRepository<T> where T : IDistinguishableEntity
    {
        Task<T> GetAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByUserAsync(string userId);
    }
}
