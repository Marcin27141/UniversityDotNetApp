using ApiDtoLibrary.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.Contracts
{
    public interface IAuthenticationRepository
    {
        Task<IdentityResult> AddClaimAsync(string userId, Claim claim);

        bool ConfirmedAccountRequired();

        Task<IdentityResult> CreateUserAsync(ApiUser user, string password);
        Task<string> GetUserIdByUsernameAsync(string username);

        Task<string> GenerateEmailConfirmationTokenAsync(string userId);

        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();

        Task<SignInResult> PasswordSignInAsync(LoginDto loginDto, bool rememberMe, bool lockoutOnFailure);

        Task SignInAsync(string userId, bool isPersistent);
    }
}
