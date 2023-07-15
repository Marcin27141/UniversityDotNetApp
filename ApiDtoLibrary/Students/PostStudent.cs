using ApiDtoLibrary.Person;
using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Students
{
    public class PostStudent : ToApiStudent
    {
        [Required]
        [Display(Name = "Id")]
        public string ApplicationUserId { get; set; }

        public PersonStatus PersonStatus { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }
    }
}
