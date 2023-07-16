

using ApiDtoLibrary.Professors;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ApiDtoLibrary.Courses
{
    public class PostCourse : BaseCourse
    {
        [Required]
        public string Name { get; set; }
        public int ECTS { get; set; }

        [Display(Name = "Is finished with exam?")]
        public bool IsFinishedWithExam { get; set; }

        public BaseProfessor Professor { get; set; }
    }
}
