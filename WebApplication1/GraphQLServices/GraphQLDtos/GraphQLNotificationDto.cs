using ApiDtoLibrary.Person;
using System;

namespace WebApplication1.GraphQLServices.GraphQLDtos
{
    public class GraphQLNotificationDto
    {
        public Guid Id { get; set; }
        public GraphQLPersonDto Recipient { get; set; }
        public Guid RecipientId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsNew { get; set; }
    }
}
