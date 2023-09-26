using ApiDtoLibrary.Person;
using System;

namespace WebApplication1.GraphQLServices.GraphQLDtos
{
    public class GraphQLProfessorDto : GraphQLPersonDto
    {
        public string ProfessorId { get; set; }
        public string Subject { get; set; }
        public DateTime FirstDayAtJob { get; set; }
        public int Salary { get; set; }
    }
}
