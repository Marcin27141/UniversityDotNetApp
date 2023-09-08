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

            descriptor.BindFieldsExplicitly();
            descriptor.Field(c => c.EntityCourseID).Name("Id");
            descriptor.Field(c => c.CourseCode).Name("CourseCode");
            descriptor.Field(c => c.Name).Name("Name");
            descriptor.Field(c => c.ECTS).Name("ECTS");
            descriptor.Field(c => c.IsFinishedWithExam).Name("IsFinishedWithExam");
            descriptor.Field(c => c.SoftDeleted).Ignore();

            descriptor.Field(c => c.Students)
                .ResolveWith<Resolvers>(c => c.GetStudents(default!, default!))
                .Description("This is a list of students enrolled for this course")
                .Name("EnrolledStudents");

            descriptor.Field(c => c.Professor)
                .ResolveWith<Resolvers>(c => c.GetProfessor(default!, default!))
                .Description("This is a professor teaching this course")
                .Name("Professor");
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
