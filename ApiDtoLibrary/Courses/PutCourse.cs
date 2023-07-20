using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;

namespace ApiDtoLibrary.Courses
{
    public class PutCourse : ToApiCourse
    {
        [Required]
        [Display(Name = "Id")]
        public Guid EntityCourseId { get; set; }

        public int ECTS { get; set; }

        [Display(Name = "Is finished with exam?")]
        public bool IsFinishedWithExam { get; set; }
    }
}
