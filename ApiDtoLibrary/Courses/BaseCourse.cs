using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Courses
{
    public abstract class BaseCourse
    {
        [Required]
        [RegularExpression(@"[A-Z]\d{2}", ErrorMessage = "Course code must be a capital letter and two digits")]
        [Display(Name = "Course code")]
        public string CourseCode { get; set; }
    }
}
