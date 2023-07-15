using ApiDtoLibrary.Users;
using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Person
{
    public class PostPersonDto : BasePersonDto
    {
        public string ApplicationUserId { get; set; }

        public PersonStatus PersonStatus { get; set; }
    }
}
