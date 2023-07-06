using Microsoft.AspNetCore.Identity;

namespace UniversityApi.API.DataBase.Identity
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
