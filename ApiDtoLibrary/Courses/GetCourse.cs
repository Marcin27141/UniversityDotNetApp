using System.ComponentModel.DataAnnotations;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;

namespace ApiDtoLibrary.Courses
{
    public class GetCourse : BaseGetCourse
    {
        public int ECTS { get; set; }

        [Display(Name = "Is finished with exam?")]
        public bool IsFinishedWithExam { get; set; }

        public BaseProfessor Professor { get; set; }

        public List<BaseGetStudent> Students { get; set; }
    }
}
