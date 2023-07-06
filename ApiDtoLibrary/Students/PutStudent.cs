using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Courses;

namespace ApiDtoLibrary.Students
{
    public class PutStudent : BaseStudent
    {
        [Required]
        [Display(Name = "Id")]
        public int EntityPersonID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }

        public List<Course> Courses { get; set; }
    }
}
