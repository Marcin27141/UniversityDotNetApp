using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;

namespace ApiDtoLibrary.Courses
{
    public class Course : BaseCourse
    {
        [Required]
        [Display(Name = "Id")]
        public int EntityCourseID { get; set; }

        public Professor Professor { get; set; }

        public List<Student> EnrolledStudents { get; set; }
    }
}
