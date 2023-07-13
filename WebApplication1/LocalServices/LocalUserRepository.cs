using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Database;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.LocalServices
{
    public class LocalUserRepository : IUserRepository
    {
        private readonly WebAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly SignInManager<WebAppUser> _signInManager;
        private readonly UserManager<WebAppUser> _userManager;

        public LocalUserRepository(WebAppDbContext context,
            IMapper mapper,
            SignInManager<WebAppUser> signInManager,
            UserManager<WebAppUser> userManager
            )
        {
            _context = context;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<ApplicationUser>>(users);
        }

        public async Task<ApplicationUser> GetUserAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            return _mapper.Map<ApplicationUser>(user);
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }
    }
}
