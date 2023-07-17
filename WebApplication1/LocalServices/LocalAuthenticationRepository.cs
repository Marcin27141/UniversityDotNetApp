using ApiDtoLibrary.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Database;
using WebApplication1.Services;

namespace WebApplication1.LocalServices
{
    public class LocalAuthenticationRepository : IAuthenticationRepository
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<WebAppUser> _signInManager;
        private readonly UserManager<WebAppUser> _userManager;
        public LocalAuthenticationRepository(IMapper mapper, SignInManager<WebAppUser> signInManager, UserManager<WebAppUser> userManager)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IdentityResult> AddClaimAsync(string userId, Claim claim)
        {
            var appUser = await _userManager.FindByIdAsync(userId);
            return await _userManager.AddClaimAsync(appUser, claim);
        }

        public bool ConfirmedAccountRequired()
        {
            return _userManager.Options.SignIn.RequireConfirmedAccount;
        }

        public Task<IdentityResult> CreateUserAsync(ApplicationUser user)
        {
            var appUser = new WebAppUser { Email = user.Email, UserName = user.Email };
            return _userManager.CreateAsync(appUser, user.Password);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var appUser = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
        }

        public Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public async Task<string> GetIdByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user.Id;
        }

        public Task<SignInResult> PasswordSignInAsync(ApplicationUser user, bool rememberMe, bool lockoutOnFailure)
        {
            return _signInManager.PasswordSignInAsync(user.Email, user.Password, rememberMe, lockoutOnFailure);
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            var appUser = await _userManager.FindByIdAsync(user.Id);
            await _signInManager.SignInAsync(appUser, isPersistent);
        }
    }
}
