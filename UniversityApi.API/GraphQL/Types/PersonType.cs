using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL.Types
{
    public class PersonType : ObjectType<EntityPerson>
    {
        protected override void Configure(IObjectTypeDescriptor<EntityPerson> descriptor)
        {
            //descriptor.Field(p => p.SoftDeleted).Ignore();
        }
    }
}
