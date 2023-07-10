using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;

namespace ApiDtoLibrary.Courses
{
    public class FullCourse : BaseCourse
    {
        [Required]
        [Display(Name = "Id")]
        public Guid EntityCourseID { get; set; }

        public FullProfessor Professor { get; set; }

        public List<FullStudent> EnrolledStudents { get; set; }
    }
}
