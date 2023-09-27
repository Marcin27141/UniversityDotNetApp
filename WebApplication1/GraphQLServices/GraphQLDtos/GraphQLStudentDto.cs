using System;
using System.Collections.Generic;

namespace WebApplication1.GraphQLServices.GraphQLDtos
{
    public class GraphQLStudentDto : GraphQLPersonDto
    {
        public string Index { get; set; }
        public DateTime BeginningOfStudying { get; set; }
        public List<GraphQLCourseDto> Courses { get; set; }
    }
}
