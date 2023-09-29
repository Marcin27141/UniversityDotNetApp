using ApiDtoLibrary.Person;
using System;
using System.Collections.Generic;

namespace WebApplication1.GraphQLServices.GraphQLDtos
{
    public class GraphQLPersonDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PESEL { get; set; }
        public DateTime Birthday { get; set; }
        public string Motherland { get; set; }
        public PersonStatus PersonStatus { get; set; }
        public List<GraphQLNotificationDto> Notifications { get; set; }
    }
}
