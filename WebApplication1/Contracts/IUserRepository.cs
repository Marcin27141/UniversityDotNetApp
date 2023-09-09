using ApiDtoLibrary.Users;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Contracts
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetUnsetNonadminUsersAsync();
        Task<ApplicationUser> GetUserAsync(string id);
        bool IsSignedIn(ClaimsPrincipal user);
        Task<IdentityResult> DeleteUserAsync(Person person);
        Task DeletePersonAsync(Person person);
    }
}
