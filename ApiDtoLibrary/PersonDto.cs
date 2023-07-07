using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary
{
    public class PersonDto
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\d{11}", ErrorMessage = "PESEL must be 11 digits")]
        public string PESEL { get; set; }

        public DateTime Birthday { get; set; }

        public string Motherland { get; set; }
    }
}
