using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Security.Claims;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly UniversityApiDbContext _context;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly UserManager<ApiUser> _userManager;

        public UserRepository(UniversityApiDbContext context, SignInManager<ApiUser> signInManager, UserManager<ApiUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public Task<List<ApiUser>> GetAllUsersAsync()
        {
            return _context.Users.ToListAsync();
        }

        public async Task<ApiUser> GetUserAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool IsSignedIn(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var claimsPrincipal = _signInManager.CreateUserPrincipalAsync(user).Result;
            return _signInManager.IsSignedIn(claimsPrincipal);
        }
    }
}
