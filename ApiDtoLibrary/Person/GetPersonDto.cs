using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Person
{
    public class GetPersonDto : PostPersonDto
    {
        public Guid EntityPersonId { get; set; }
    }
}
