using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Services;

namespace WebApplication1.Database
{
    public class WebAppDbContext : IdentityDbContext<WebAppUser>
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
        {
                
        }
    }
}
