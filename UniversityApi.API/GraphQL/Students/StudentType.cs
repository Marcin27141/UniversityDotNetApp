using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase;
using Microsoft.EntityFrameworkCore;

namespace UniversityApi.API.GraphQL.Students
{
    public class StudentType : ObjectType<EntityStudent>
    {
        protected override void Configure(IObjectTypeDescriptor<EntityStudent> descriptor)
        {
            descriptor.Description("Represents a person studying at the university");

            descriptor.Field(s => s.SoftDeleted).Ignore();

            descriptor.Field(s => s.Courses)
                .ResolveWith<Resolvers>(s => s.GetCourses(default!, default!))
                .Description("This is a list of courses, that a student is enrolled for");
        }

        private class Resolvers
        {
            public IList<EntityCourse> GetCourses([Parent] EntityStudent student, UniversityApiDbContext context)
            {
                var studentWithCourses = context
                    .Set<EntityStudent>()
                    .Include(s => s.Courses)
                        .ThenInclude(c => c.Professor)
                    .SingleOrDefault(s => s.EntityPersonID == student.EntityPersonID);
                return studentWithCourses.Courses;
            }
        }
    }
}
