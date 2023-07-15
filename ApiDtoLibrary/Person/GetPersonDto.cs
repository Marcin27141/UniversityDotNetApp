using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Person
{
    public class GetPersonDto : BasePersonDto
    {
        public Guid EntityPersonId { get; set; }

        public string ApplicationUserId { get; set; }

        public PersonStatus PersonStatus { get; set; }
    }
}
