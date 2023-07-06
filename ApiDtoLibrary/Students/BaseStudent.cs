using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary;

namespace ApiDtoLibrary.Students
{
    public class BaseStudent : Person
    {
        [Required]
        [RegularExpression(@"\d{6}", ErrorMessage = "Index must be 6 digits")]
        public string Index { get; set; }
    }
}
