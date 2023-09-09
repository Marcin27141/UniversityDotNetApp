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
        }
    }
}
