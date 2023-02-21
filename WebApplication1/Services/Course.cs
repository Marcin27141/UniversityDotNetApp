using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public List<Student> EnrolledStudents { get; set; }

        public override string ToString() => $"{Name} ({CourseCode})";

        public EntityCourse ToEntityCourse(EntityProfessor entityProfessor, List<EntityStudent> entityStudents)
        {
            var output = new EntityCourse()
            {
                CourseCode = CourseCode,
                Name = Name,
                Professor = entityProfessor,
                ECTS = ECTS,
                IsFinishedWithExam = IsFinishedWithExam
            };

            output.Students = new List<StudentCourse>();
            for (int i = 0; i < entityStudents.Count; i++)
                output.Students.Add(new StudentCourse
                {
                    Student = entityStudents.ElementAt(i),
                    Course = output,
                });

            return output;
        }

        public static Course FromEntityCourse(EntityCourse entityCourse)
        {
            return new Course
            {
                Name = entityCourse.Name,
                CourseCode = entityCourse.CourseCode,
                Professor = entityCourse.Professor == null ? null : Professor.FromEntityProfessor(entityCourse.Professor),
                ECTS = entityCourse.ECTS,
                IsFinishedWithExam = entityCourse.IsFinishedWithExam,
                EnrolledStudents = entityCourse.Students.Select(sc => Student.FromEntityStudentFlat(sc.Student)).ToList()
            };
        }

        public static Course FromEntityCourseFlat(EntityCourse entityCourse)
        {
            return new Course
            {
                Name = entityCourse.Name,
                CourseCode = entityCourse.CourseCode,
                ECTS = entityCourse.ECTS,
                IsFinishedWithExam = entityCourse.IsFinishedWithExam,
            };
        }
    }
}
