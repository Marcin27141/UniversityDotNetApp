using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL.Types
{
    public class ProfessorType : ObjectType<EntityProfessor>
    {
        protected override void Configure(IObjectTypeDescriptor<EntityProfessor> descriptor)
        {
            descriptor.Description("Represents a person teaching at the university");

            descriptor.BindFieldsExplicitly();
            ObjectTypeDescriptorHelper<EntityProfessor>.ConfigurePersonalData(descriptor);
            descriptor.Field(p => p.IdCode).Name("ProfessorId");
            descriptor.Field(p => p.Subject).Name("Subject");
            descriptor.Field(p => p.FirstDayAtJob).Name("FirstDayAtJob");
            descriptor.Field(p => p.Salary).Name("Salary");
        }
    }
}
