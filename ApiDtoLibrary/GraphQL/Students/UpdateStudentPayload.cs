using ApiDtoLibrary.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtoLibrary.GraphQL.Students
{
    public record UpdateStudentPayload(
        GetStudent getStudent
        );
}
