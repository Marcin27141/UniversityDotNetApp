using System.ComponentModel.DataAnnotations;

namespace GrpcService.Models
{
    public class Grade
    {
        public Guid GradeId { get; set; }
        public decimal GradeValue { get; set; }
        public DateTime DateOfGradeSubmision { get; set; }

        [Required]
        public Student GradedStudent { get; set; }
        public Guid GradedStudentId { get; set; }

        [Required]
        public Course GradedCourse { get; set; }
        public Guid GradedCourseId { get; set; }
        
    }
}
