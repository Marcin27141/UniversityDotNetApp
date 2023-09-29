using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL.Types
{
    public class NotificationType : ObjectType<EntityNotification>
    {
        protected override void Configure(IObjectTypeDescriptor<EntityNotification> descriptor)
        {
            descriptor.Description("Represents a piece of information passed in the university system");

            descriptor.Field(n => n.EntityNotificationId).Name("Id");
            descriptor.Field(n => n.Title).Name("Title");
            descriptor.Field(n => n.Body).Name("Body");
            descriptor.Field(n => n.RecipientId).Name("RecipientId");
            descriptor.Field(n => n.IsNew).Name("IsNew");

            descriptor.Field(n => n.Recipient)
                .ResolveWith<Resolvers>(n => n.GetRecipient(default!, default!))
                .Description("The addressee of the message");
        }

        private class Resolvers
        {
            public EntityPerson GetRecipient([Parent] EntityNotification notification, UniversityApiDbContext context)
            {
                return context.People.Find(notification.RecipientId);
            }
        }
    }
}
