using ApiDtoLibrary.Courses;
using ApiDtoLibrary.GraphQL.Courses;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase;
using ApiDtoLibrary.GraphQL.Professors;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.GraphQL.People;

namespace UniversityApi.API.GraphQL.Mutations
{
    public partial class Mutation
    {
        public async Task<AddCoursePayload> AddCourseAsync(AddCourseInput input,
            UniversityApiDbContext dbContext)
        {
            var course = _mapper.Map<EntityCourse>(input.postCourse);

            await dbContext.AddAsync(course);
            await dbContext.SaveChangesAsync();

            return new AddCoursePayload(_mapper.Map<GetCourse>(course));
        }

        public async Task<UpdateCoursePayload> UpdateCourseAsync(UpdateCourseInput input,
            UniversityApiDbContext dbContext)
        {
            var putCourse = input.putCourse;
            var toUpdate = dbContext.Courses.Find(putCourse.EntityCourseId) ?? throw new KeyNotFoundException($"Course with id {putCourse.EntityCourseId} was not found");
            _mapper.Map(putCourse, toUpdate);
            if (putCourse.ProfessorId != null)
                toUpdate.Professor = await dbContext.Set<EntityProfessor>().FindAsync(putCourse.ProfessorId);

            await dbContext.SaveChangesAsync();

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
