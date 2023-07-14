using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Services.People
{
    public class Student : Person
    {
        public static readonly int STUDENT_ENTITY_CLASS_ID = 3;


        [Required]
        [RegularExpression(@"\d{6}", ErrorMessage = "Please use a 6 digit index")]
        public string Index { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First day of studying")]
        public DateTime BeginningOfStudying { get; set; }

        public List<Course> Courses { get; set; }

        public override string ToString() => PersonalData.ToString();
    }
}
