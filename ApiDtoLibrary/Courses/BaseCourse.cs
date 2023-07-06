using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Courses
{
    public abstract class BaseCourse
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"[A-Z]\d{2}", ErrorMessage = "Course code must be a capital letter and two digits")]
        [Display(Name = "Course code")]
        public string CourseCode { get; set; }
        public int ECTS { get; set; }

        [Display(Name = "Is finished with exam?")]
        public bool IsFinishedWithExam { get; set; }
    }
}
