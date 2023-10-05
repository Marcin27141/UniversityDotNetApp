using System;
using WebApplication1.Services.People;

namespace WebApplication1.Services
{
    public class CourseGrade
    {
        public Guid GradeId { get; set; }
        public float GradeValue { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime TimeOfAdding { get; set; }
    }
}
