using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL.Types
{
    public class CourseType : ObjectType<EntityCourse>
    {
        protected override void Configure(IObjectTypeDescriptor<EntityCourse> descriptor)
        {
            descriptor.Description("Represents a syllabus item offered by the university");

            descriptor.Field(c => c.SoftDeleted).Ignore();

            descriptor.Field(c => c.Students)
                .ResolveWith<Resolvers>(c => c.GetStudents(default!, default!))
                .Description("This is a list of students enrolled for this course");

            descriptor.Field(c => c.Professor)
                .ResolveWith<Resolvers>(c => c.GetProfessor(default!, default!))
                .Description("This is a professor teaching this course");
        }

        private class Resolvers
        {
            public IList<EntityStudent> GetStudents([Parent] EntityCourse course, UniversityApiDbContext context)
            {
                var courseWithStudents = context
                    .Courses
                    .Include(c => c.Students)
                    .SingleOrDefault(c => c.EntityCourseID == course.EntityCourseID);
                return courseWithStudents.Students;
            }

            public EntityProfessor GetProfessor([Parent] EntityCourse course, UniversityApiDbContext context)
            {
                var courseWithProfessor = context
                    .Courses
                    .Include(c => c.Professor)
                    .SingleOrDefault(c => c.EntityCourseID == course.EntityCourseID);
                return courseWithProfessor.Professor;
            }
        }
    }
}
