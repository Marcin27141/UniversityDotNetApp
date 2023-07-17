using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface IGenericPostRepository<T, U> where T : IDistinguishableEntity
    {
        Task AddClaimAfterPostAsync(string userId, Claim claim);
        Task<U> AddAsync(T entity);
    }
}
