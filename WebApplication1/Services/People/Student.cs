using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.PeopleOps;
using WebApplication1.Services.StudentOps;

namespace WebApplication1.Services.People
{
    public class Student
    {
        public PersonalData PersonalData { get; set; }

        [Required]
        [RegularExpression(@"\d{6}", ErrorMessage = "Please use a 6 digit index")]
        public string Index { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }

        public List<Course> Courses { get; set; }

        public override string ToString() => PersonalData.ToString();

        public EntityStudent ToEntityStudent(List<EntityCourse> courses)
        {
            var output = new EntityStudent()
            {
                StudentIndex = this.Index,
                PersonalData = this.PersonalData.ToEntityPersonalData(this.Index,PersonType.Student),
                BeginningOfStudying = this.BeginningOfStudying
            };

            output.Courses = new List<StudentCourse>();
            for (int i = 0; i < courses.Count; i++)
                output.Courses.Add(new StudentCourse
                {
                    Student = output,
                    Course = courses.ElementAt(i),
                });

            return output;
        }

        public static Student FromEntityStudent(EntityStudent entityStudent)
        {
            return new Student
            {
                PersonalData = PersonalData.FromEntityPersonalData(entityStudent.PersonalData),
                Index = entityStudent.StudentIndex,
                BeginningOfStudying = entityStudent.BeginningOfStudying,
                Courses = entityStudent.Courses.Select(sc => Course.FromEntityCourse(sc.Course)).ToList()
            };
        }
    }
}
