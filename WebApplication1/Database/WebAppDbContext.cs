using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Database
{
    public class WebAppDbContext : IdentityDbContext<WebAppUser>
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
        {
                
        }
    }
}
