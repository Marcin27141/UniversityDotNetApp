using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Courses;

namespace ApiDtoLibrary.Students
{
    public class PutStudent : ToApiStudent
    {
        [Required]
        [Display(Name = "Id")]
        public Guid EntityPersonID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }
    }
}
