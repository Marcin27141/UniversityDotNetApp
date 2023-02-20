using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.CourseOps;
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

        public int ECTS { get; set; }

        [Display(Name="Is finished with exam?")]
        public bool IsFinishedWithExam { get; set; }

        public override string ToString() => $"{Name} ({CourseCode})";

        public EntityCourse ToEntityCourse(EntityProfessor entityProfessor)
        {
            return new EntityCourse()
            {
                CourseCode = CourseCode,
                Name = Name,
                Professor = entityProfessor,
                ECTS = ECTS,
                IsFinishedWithExam = IsFinishedWithExam
            };
        }

        public static Course FromEntityCourse(EntityCourse entityCourse)
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
