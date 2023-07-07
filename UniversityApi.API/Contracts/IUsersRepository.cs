using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.Contracts
{
    public interface IUsersRepository
    {
        Task<List<ApiUser>> GetAllUsersAsync();
        Task<ApiUser> GetUserByIdAsync(string id);
    }
}
