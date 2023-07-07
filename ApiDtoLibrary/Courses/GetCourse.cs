using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Professors;

namespace ApiDtoLibrary.Courses
{
    public class GetCourse : BaseCourse
    {
        [Required]
        [Display(Name = "Id")]
        public int EntityCourseID { get; set; }
        public FullProfessor Professor { get; set; }
    }
}
