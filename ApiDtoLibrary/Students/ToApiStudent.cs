using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtoLibrary.Students
{
    public class ToApiStudent : BaseStudent
    {
        public List<Guid> CoursesIds { get; set; }
    }
}
