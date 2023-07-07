using ApiDtoLibrary.Users;
using System.Collections.Generic;
using WebApplication1.Services;

namespace WebApplication1.Contracts
{
    public interface IUserRepository
    {
        List<ApplicationUser> GetAllUsers();
        ApplicationUser GetUserById(string id);
    }
}
