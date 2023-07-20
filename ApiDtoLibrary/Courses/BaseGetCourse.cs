using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApiDtoLibrary.Courses
{
    public class BaseGetCourse : BaseCourse
    {
        [Required]
        [Display(Name = "Id")]
        public Guid EntityCourseId { get; set; }
    }
}
