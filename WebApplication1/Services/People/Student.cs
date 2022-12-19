using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.PeopleOps;

namespace WebApplication1.Services.People
{
    public class Student
    {
        public PersonalData PersonalData { get; set; }

        [RegularExpression(@"\d{6}", ErrorMessage = "Please use a 6 digit index")]
        public string Index { get; set; }

        public float Average { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }

        public List<Course> Courses { get; set; }

        public override string ToString() => PersonalData.ToString();

        public DataBase.Entities.Student ToEntityStudent(List<DataBase.Entities.Course> courses)
        {
            var output = new DataBase.Entities.Student()
            {
                StudentIndex = this.Index,
                PersonalData = this.PersonalData.ToEntityPersonalData(this.Index,PersonType.Student),
                Average = this.Average,
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

        public static Student FromEntityStudent(DataBase.Entities.Student entityStudent)
        {
            return new Student
            {
                PersonalData = PersonalData.FromEntityPersonalData(entityStudent.PersonalData),
                Index = entityStudent.StudentIndex,
                Average = entityStudent.Average,
                BeginningOfStudying = entityStudent.BeginningOfStudying,
                Courses = entityStudent.Courses.Select(sc => Course.FromEntityCourse(sc.Course)).ToList()
            };
        }
    }
}
