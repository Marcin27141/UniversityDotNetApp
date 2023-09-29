using ApiDtoLibrary.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtoLibrary.Notifications
{
    public class GetNotificationDto
    {
        public Guid EntityNotificationId { get; set; }
        public GetPersonDto Recipient { get; set; }
        public Guid RecipientId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsNew { get; set; }
    }
}
