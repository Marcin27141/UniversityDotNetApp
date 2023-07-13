using ApiDtoLibrary.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly UserManager<ApiUser> _userManager;

        public AuthenticationRepository(SignInManager<ApiUser> signInManager, UserManager<ApiUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddClaimAsync(string userId, Claim claim)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.AddClaimAsync(user, claim);
        }

        public bool ConfirmedAccountRequired()
        {
            //return _userManager.Options.SignIn.RequireConfirmedAccount;
            return false;
        }

        public Task<IdentityResult> CreateUserAsync(ApiUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public async Task<string> GetUserIdByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user.Id;
        }

        public async Task<SignInResult> PasswordSignInAsync(LoginDto user, bool rememberMe, bool lockoutOnFailure)
        {
            //TODO rememberMe to be implemented
            var apiUser = await _userManager.FindByEmailAsync(user.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(apiUser, user.Password, lockoutOnFailure);
            return result;
        }

        public async Task SignInAsync(string userId, bool isPersistent)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _signInManager.SignInAsync(user, isPersistent);
        }
    }
}
