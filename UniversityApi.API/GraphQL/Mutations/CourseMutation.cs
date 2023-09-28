using ApiDtoLibrary.Courses;
using ApiDtoLibrary.GraphQL.Courses;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase;
using ApiDtoLibrary.GraphQL.Professors;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.GraphQL.People;
using HotChocolate.Subscriptions;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UniversityApi.API.GraphQL.Mutations
{
    public partial class Mutation
    {
        public async Task<AddCoursePayload> AddCourseAsync(AddCourseInput input,
            UniversityApiDbContext dbContext,
            ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var course = _mapper.Map<EntityCourse>(input.postCourse);
            if (course.ProfessorId != null)
                course.Professor = await dbContext.Set<EntityProfessor>().FindAsync(course.ProfessorId);

            await dbContext.AddAsync(course);
            await dbContext.SaveChangesAsync(cancellationToken);

            if (course.Professor != null)
                await eventSender.SendAsync(nameof(Subscription.OnCourseProfessorAssignment), course, cancellationToken);

            return new AddCoursePayload(_mapper.Map<GetCourse>(course));
        }

        public async Task<UpdateCoursePayload> UpdateCourseAsync(UpdateCourseInput input,
            UniversityApiDbContext dbContext,
            ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var putCourse = input.putCourse;
            var toUpdate = dbContext.Courses.Find(putCourse.EntityCourseId) ?? throw new KeyNotFoundException($"Course with id {putCourse.EntityCourseId} was not found");
            _mapper.Map(putCourse, toUpdate);
            if (putCourse.ProfessorId != null)
                toUpdate.Professor = await dbContext.Set<EntityProfessor>().FindAsync(putCourse.ProfessorId);

            await dbContext.SaveChangesAsync(cancellationToken);

            if (toUpdate.Professor != null)
                await eventSender.SendAsync(nameof(Subscription.OnCourseProfessorAssignment), toUpdate, cancellationToken);

            var getCourse = _mapper.Map<GetCourse>(toUpdate);
            return new UpdateCoursePayload(getCourse);
        }

        public async Task<DeleteCoursePayload> DeleteCourseAsync(DeleteCourseInput input,
            UniversityApiDbContext dbContext)
        {
            if (!Guid.TryParse(input.Id, out Guid courseId))
                return new DeleteCoursePayload(false);

            var course = await dbContext.Courses.FindAsync(courseId);
            if (course != null)
            {
                dbContext.Remove(course);
                await dbContext.SaveChangesAsync();
                return new DeleteCoursePayload(true);
            }

            return new DeleteCoursePayload(false);
        }
    }
}
