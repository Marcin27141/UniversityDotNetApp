using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Person;

namespace ApiDtoLibrary.Professors
{
    public class BaseProfessor : BasePersonDto
    {
        [Required]
        [RegularExpression(@"\d{5}", ErrorMessage = "ID must be 5 digits")]
        [Display(Name = "Id code")]
        public string IdCode { get; set; }

        public string Subject { get; set; }
    }
}
