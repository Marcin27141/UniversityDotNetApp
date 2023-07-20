using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtoLibrary.Courses
{
    public class ToApiCourse : BaseCourse
    {
#nullable enable
        public Guid? ProfessorId { get; set; }
    }
}
