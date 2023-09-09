using ApiDtoLibrary;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.Repositories
{
    public class PeopleRepository : IPeopleRespository
    {
        private readonly UniversityApiDbContext _context;

        public PeopleRepository(UniversityApiDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<EntityPerson> GetAsync(Guid id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            _context.People.Remove(entity);     
            await _context.SaveChangesAsync();
        }

        public async Task<List<EntityPerson>> GetAllPersonalDataAsync()
        {
            return await _context.People.ToListAsync();
        }
    }
}
