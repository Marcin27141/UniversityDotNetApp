using ApiDtoLibrary.Person;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Models
{
    public class PersonalData
    {
        public Guid PersonId { get; set; }
        public Guid ApplicationUserId { get; set; } = Guid.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PESEL { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Motherland { get; set; }
        public PersonStatus PersonStatus { get; set; } = PersonStatus.Unidentified;
    }
}
