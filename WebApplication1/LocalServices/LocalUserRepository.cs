using ApiDtoLibrary.Person;
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
        private readonly IPeopleRepository _peopleRepository;

        public LocalUserRepository(WebAppDbContext context,
            IMapper mapper,
            SignInManager<WebAppUser> signInManager,
            UserManager<WebAppUser> userManager,
            IPeopleRepository peopleRepository
            )
        {
            _context = context;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            this._peopleRepository = peopleRepository;
        }
        public async Task<List<ApplicationUser>> GetUnsetNonadminUsersAsync()
        {
            //var users = await _context.Users.ToListAsync();
            //return _mapper.Map<List<ApplicationUser>>(users);

            var users = await _context.Users.ToListAsync();
            var result = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);

                var isAdmin = userClaims.Any(c => c.Type == "IsAdmin");
                var isSet = userClaims.Any(c => c.Type == "EntityPersonId");
                var status = userClaims.FirstOrDefault(c => c.Type == "Status")?.Value;

                if (!isAdmin && !isSet)
                {
                    var applicationUser = _mapper.Map<ApplicationUser>(user);
                    applicationUser.Status = Enum.Parse<PersonStatus>(status); // Assign the "Status" property
                    result.Add(applicationUser);
                }
            }

            return result;
        }

        public async Task<ApplicationUser> GetUserAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            return _mapper.Map<ApplicationUser>(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(Person person)
        {
            var user = await _context.Users.FindAsync(person.ApplicationUserId);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                await DeletePersonAsync(person);
            return result;
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }

        public async Task DeletePersonAsync(Person person)
        {
            await _peopleRepository.DeleteAsync(person.EntityPersonID);
        }
    }
}
