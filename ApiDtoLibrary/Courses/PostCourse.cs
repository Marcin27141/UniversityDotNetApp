

using ApiDtoLibrary.Professors;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ApiDtoLibrary.Courses
{
    public class PostCourse : ToApiCourse
    {
        public int ECTS { get; set; }

        [Display(Name = "Is finished with exam?")]
        public bool IsFinishedWithExam { get; set; }
    }
}
