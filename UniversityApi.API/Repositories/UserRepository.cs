using Microsoft.EntityFrameworkCore;
using System;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly UniversityApiDbContext _context;
        public UserRepository(UniversityApiDbContext context)
        {
            _context = context;
        }
        public Task<List<ApiUser>> GetAllUsersAsync()
        {
            return _context.Users.ToListAsync();
        }

        public async Task<ApiUser> GetUserByIdAsync(string id)
        {
            var result = await _context.Users.FindAsync(id);
            return result;
        }
    }
}
