using ApiDtoLibrary.Person;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Services
{
    public class ApplicationUser
    {
        public string Id{ get; set;}

        public Guid EntityPersonId { get; set; }

        public PersonStatus Status { get; set; }

        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string FirstName { get; set;}
        public string LastName { get; set; }
        
    }
}
