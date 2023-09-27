using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtoLibrary.GraphQL.Courses
{
    public record UpdateCoursePayload(
        GetCourse getCourse
        );
}
