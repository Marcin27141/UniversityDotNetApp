using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL.Types
{
    public class ObjectTypeDescriptorHelper<T> where T : EntityPerson
    {
        public static void ConfigurePersonalData(IObjectTypeDescriptor<T> descriptor)
        {
            descriptor.Field(p => p.EntityPersonID).Name("Id");
            descriptor.Field(p => p.ApplicationUserId).Name("UserId");
            descriptor.Field(p => p.FirstName).Name("FirstName");
            descriptor.Field(p => p.LastName).Name("LastName");
            descriptor.Field(p => p.PESEL).Name("PESEL");
            descriptor.Field(p => p.Birthday).Name("Birthday");
            descriptor.Field(p => p.Motherland).Name("Motherland");
            descriptor.Field(p => p.PersonStatus).Name("PersonStatus");

            descriptor.Field(p => p.Notifications)
                .ResolveWith<Resolvers>(c => c.GetNotifications(default!, default!))
                .Description("Notifications sent to given person")
                .Name("Notifications");
        }

        private class Resolvers
        {
            public IList<EntityNotification> GetNotifications([Parent] EntityPerson person, UniversityApiDbContext context)
            {
                var personWithNotifications = context
                    .People
                    .Include(p => p.Notifications)
                    .SingleOrDefault(p => p.EntityPersonID == person.EntityPersonID);
                return personWithNotifications.Notifications;
            }
        }
    }
}
