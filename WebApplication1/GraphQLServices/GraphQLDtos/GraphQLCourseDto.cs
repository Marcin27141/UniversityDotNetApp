using ApiDtoLibrary.Person;
using System;
using System.Collections.Generic;

namespace WebApplication1.GraphQLServices.GraphQLDtos
{
    public class GraphQLCourseDto
    {
        public Guid Id { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public int ECTS { get; set; }
        public bool IsFinishedWithExam { get; set; }
        public List<GraphQLStudentDto> EnrolledStudents { get; set; }
        public GraphQLProfessorDto Professor { get; set; }
    }
}
