using ApiDtoLibrary.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserAsync(string id);
    }
}
