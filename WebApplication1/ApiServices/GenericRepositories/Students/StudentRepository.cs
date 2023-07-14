using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApi.API.Contracts;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Extensions;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices.GenericRepositories.Students
{
    public class StudentRepository : GenericRepository<Student>, IStudentsRepository
    {
        public StudentRepository(IMapper mapper, IGenericGetRepository<Student> getRepository, IGenericPostRepository<Student> postRepository, IGenericPutRepository<Student> putRepository) : base(mapper, getRepository, postRepository, putRepository)
        {
            _apiPath += ApiPathAppendixDictionary.GetValue(ApiGenericTypes.Student);
        }

        public async Task<bool> IndexIsOccupied(string index)
        {
            string checkIndexPath = $"{_apiPath}/IndexCheck/{index}";
            var response = await _httpClient.GetAsync(checkIndexPath);
            if (response.IsSuccessStatusCode)
            {
                return bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return default;
        }

        public async Task RemoveStudentCourseAsync(Guid studentId, Guid courseId)
        {
            string deletePath = $"{_apiPath}/{studentId}/{courseId}";
            await _httpClient.DeleteAsync(deletePath);
        }

        public List<Student> SortFilterStudents(StudentOrderByOptions orderByOption, StudentFilterByOptions filterByOption, string filter)
        {
            var allStudents = GetAllAsync().Result;
            return allStudents
                .OrderStudentsBy(orderByOption)
                .FilterStudentsBy(filterByOption, filter)
                .ToList();
        }

        public Task<Guid> UpdateStudentWithCoursesAsync(Student updatedStudent, IEnumerable<Guid> coursesIds)
        {
            //var postEntity = _mapper.Map<PostStudent>(updatedStudent);
            //var serializedContent = GetSerializedContent(new
            //{
            //    Object1Serialized = GetSerializedContent(postEntity),
            //    Object2Serialized = GetSerializedContent(anotherObject)
            //};);
            //string updatePath = $"{_apiPath}/Students/Courses/{updatedStudent.EntityPersonID}";
            //var response = await _httpClient.PutAsync(updatePath, serializedContent);
            //if (response.IsSuccessStatusCode)
            //    return updatedEntity.EntityId;
            return default;
        }
    }
}
