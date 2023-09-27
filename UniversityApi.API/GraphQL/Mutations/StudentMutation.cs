using ApiDtoLibrary.GraphQL.Professors;
using ApiDtoLibrary.Professors;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase;
using ApiDtoLibrary.GraphQL.Students;
using ApiDtoLibrary.Students;
using Microsoft.EntityFrameworkCore;

namespace UniversityApi.API.GraphQL.Mutations
{
    public partial class Mutation
    {
        public async Task<AddStudentPayload> AddStudentAsync(AddStudentInput input,
            UniversityApiDbContext dbContext)
        {
            var student = _mapper.Map<EntityStudent>(input.postStudent);
            var studentsCourses = input.postStudent.CoursesIds
                .Select(id => dbContext.Courses.Find(id))
                .ToList();
            student.Courses = studentsCourses;

            await dbContext.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return new AddStudentPayload(_mapper.Map<GetStudent>(student));
        }

        public async Task<UpdateStudentPayload> UpdateStudentAsync(UpdateStudentInput input,
            UniversityApiDbContext dbContext)
        {
            var putStudent = input.putStudent;

            var updatedCourses = input.putStudent.CoursesIds
                .Select(id => dbContext.Courses.Find(id))
                .ToList();
            
            var toUpdate = dbContext.Set<EntityStudent>()
                .Include(s => s.Courses)
                .SingleOrDefault(s => s.EntityPersonID == putStudent.EntityPersonId) ?? throw new KeyNotFoundException($"Student with id {putStudent.EntityPersonId} was not found");
            _mapper.Map(putStudent, toUpdate);
            toUpdate.Courses = updatedCourses;
            
            await dbContext.SaveChangesAsync();

            var getStudent = _mapper.Map<GetStudent>(toUpdate);
            return new UpdateStudentPayload(getStudent);
        }

        public async Task<RemoveStudentCoursePayload> RemoveStudentCourseAsync(RemoveStudentCourseInput input, UniversityApiDbContext dbContext)
        {
            if (!Guid.TryParse(input.studentId, out Guid studentGuid) || !Guid.TryParse(input.courseId, out Guid courseGuid))
                return new RemoveStudentCoursePayload(false);

            var student = await dbContext.Set<EntityStudent>().FindAsync(studentGuid);
            var course = await dbContext.Courses.FindAsync(courseGuid);

            if (student != null && course != null)
            {
                student.Courses.Remove(course);
                await dbContext.SaveChangesAsync();
                return new RemoveStudentCoursePayload(true);
            }
            return new RemoveStudentCoursePayload(false);
        }
    }
}
