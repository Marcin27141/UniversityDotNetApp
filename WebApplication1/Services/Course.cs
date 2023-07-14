using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication1.Services.People;

namespace WebApplication1.Services
{
    public class Course : IDistinguishableEntity
    {
        public static readonly int COURSE_ENTITY_CLASS_ID = 1;


        public Guid EntityCourseID { get; set; }

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

        public Guid EntityId => EntityCourseID;

        public override string ToString() => $"{Name} ({CourseCode})";

    }
}
