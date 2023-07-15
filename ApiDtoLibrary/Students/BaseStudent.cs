using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Person;

namespace ApiDtoLibrary.Students
{
    public class BaseStudent : BasePersonDto
    {
        [Required]
        [RegularExpression(@"\d{6}", ErrorMessage = "Index must be 6 digits")]
        public string Index { get; set; }
    }
}
