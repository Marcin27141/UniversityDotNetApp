using AutoMapper;
using System.Collections.Generic;
using WebApplication1.Queries;
using WebApplication1.Services.People;
using ApiDtoLibrary.Students;
using UniversityApi.API.Contracts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication1.ApiServices
{
    public class StudentsRepository : GenericRepository<Student, BaseStudent>, IStudentsRepository
    {
        public StudentsRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += "/Students";
        }

        public List<Student> SortFilterStudents(StudentOrderByOptions orderByOption, StudentFilterByOptions filterByOption, string filter)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> UpdateStudentAsync(Student updatedStudent, IEnumerable<string> coursesCodes)
        {
            var putStudent = _mapper.Map<PutStudent>(updatedStudent);
            var serializedContent = GetSerializedContent(putStudent);
            string updatePath = $"{_apiPath}/{putStudent.EntityPersonID}/courses";
            var response = await _httpClient.PutAsync(updatePath, serializedContent);
            if (response.IsSuccessStatusCode)
                return updatedStudent.Key;
            return null;
        }

        public async Task<bool> RemoveStudentCourseAsync(int studentId, string courseCode)
        {
            string deletePath = $"{_apiPath}/{studentId}/{courseCode}";
            await _httpClient.DeleteAsync(deletePath);
            return true;
        }
    }
}

