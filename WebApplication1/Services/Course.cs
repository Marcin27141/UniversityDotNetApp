using System.ComponentModel.DataAnnotations;
using WebApplication1.Services.People;

namespace WebApplication1.Services
{
    public class Course
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"[A-Z]\d{2}", ErrorMessage = "Please use a proper course code (capital letter and two digits)")]
        [Display(Name = "Course code")]
        public string CourseCode { get; set; }
 
        public Professor Professor { get; set; }

        [Required]
        public int ECTS { get; set; }

        [Required]
        [Display(Name="Is finished with exam?")]
        public bool IsFinishedWithExam { get; set; }

        public override string ToString() => $"{Name} ({CourseCode})";

        public DataBase.Entities.Course ToEntityCourse(DataBase.Entities.Professor entityProfessor)
        {
            return new DataBase.Entities.Course()
            {
                CourseCode = CourseCode,
                Name = Name,
                Professor = entityProfessor,
                ECTS = ECTS,
                IsFinishedWithExam = IsFinishedWithExam
            };
        }

        public static Course FromEntityCourse(DataBase.Entities.Course entityCourse)
        {
            return new Course
            {
                Name = entityCourse.Name,
                CourseCode = entityCourse.CourseCode,
                Professor = Professor.FromEntityProfessor(entityCourse.Professor),
                ECTS = entityCourse.ECTS,
                IsFinishedWithExam = entityCourse.IsFinishedWithExam
            };
        }

    }
}
