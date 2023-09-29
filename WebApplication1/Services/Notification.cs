using ApiDtoLibrary.Person;
using GrpcService.Models;
using System;

namespace WebApplication1.Services
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Person Recipient { get; set; }
        public Guid RecipientId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
