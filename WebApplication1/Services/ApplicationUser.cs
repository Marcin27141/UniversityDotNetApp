using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Services
{
    public class ApplicationUser
    {
        public string Id{ get; set;}
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
