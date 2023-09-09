using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Person
{
    public class BasePersonDto
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [RegularExpression(@"\d{11}", ErrorMessage = "PESEL must be 11 digits")]
        public string PESEL { get; set; }

        public DateTime Birthday { get; set; }

        public string Motherland { get; set; }
    }
}
