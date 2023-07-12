using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface IAuthenticationRepository
    {
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Task<IdentityResult> CreateUserAsync(ApplicationUser user);
        Task<IdentityResult> AddClaimAsync(ApplicationUser user, Claim claim);
        Task<string> GetIdByUsernameAsync(string username);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<SignInResult> PasswordSignInAsync(ApplicationUser user, bool rememberMe, bool lockoutOnFailure);
        Task SignInAsync(ApplicationUser user, bool isPersistent);
        bool ConfirmedAccountRequired();
    }
}
