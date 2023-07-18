using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : SoftRemovableEntity
    {
        protected readonly UniversityApiDbContext _context;
        protected readonly UserManager<ApiUser> _userManager;

        public GenericRepository(UniversityApiDbContext dbContext, UserManager<ApiUser> userManager)
        {
            _context = dbContext;
            _userManager = userManager;
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            //_context.Set<T>().Remove(entity);
            entity.SoftDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var claims = await _userManager.GetClaimsAsync(user);
            var entityPersonId = claims.FirstOrDefault(c => c.Type == "EntityPersonId")?.Value;
            return await GetAsync(Guid.Parse(entityPersonId));
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
