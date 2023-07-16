﻿using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;

namespace ApiDtoLibrary.Courses
{
    public class PutCourse : BaseCourse
    {
        [Required]
        [Display(Name = "Id")]
        public Guid EntityCourseID { get; set; }

        [Required]
        public string Name { get; set; }
        public int ECTS { get; set; }

        [Display(Name = "Is finished with exam?")]
        public bool IsFinishedWithExam { get; set; }

        public BaseProfessor Professor { get; set; }
    }
}
