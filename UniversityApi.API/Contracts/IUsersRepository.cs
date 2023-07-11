using System.Net.Http;
using System.Security.Claims;
using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.Contracts
{
    public interface IUsersRepository
    {
        Task<List<ApiUser>> GetAllUsersAsync();
        Task<ApiUser> GetUserAsync(string id);
        bool IsSignedIn(string id);
    }
}
