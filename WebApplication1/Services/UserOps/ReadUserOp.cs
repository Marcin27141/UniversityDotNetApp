using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.DataBase;
using WebApplication1.DataBase.Entities;

namespace WebApplication1.Services.UserOps
{
    public class ReadUserOp : IReadUserOp
    {
        private readonly AppDbContext _context;
        public ReadUserOp(AppDbContext context)
        {
            _context = context;
        }

        public List<ApplicationUser> GetAllUsers()
        {
            var output = new List<ApplicationUser>();
            foreach (var user in _context.Users)
            {
                output.Add(user);
            }
            return output;
        }

        public ApplicationUser GetUserById(string id)
        {
            return _context.Users.Find(id);
        }
    }

    public interface IReadUserOp
    {
        List<ApplicationUser> GetAllUsers();
        ApplicationUser GetUserById(string id);
    }
}
