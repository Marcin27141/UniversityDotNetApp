using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL.Professors
{
    public class ProfessorType : ObjectType<EntityProfessor>
    {
        protected override void Configure(IObjectTypeDescriptor<EntityProfessor> descriptor)
        {
            descriptor.Description("Represents a person teaching at the university");

            descriptor.Field(p => p.SoftDeleted).Ignore();
        }
    }
}
