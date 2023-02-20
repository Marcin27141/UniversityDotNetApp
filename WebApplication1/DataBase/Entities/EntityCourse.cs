using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DataBase.Entities
{
    public class EntityCourse
    {
        public int EntityCourseID { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public int ECTS { get; set; }
        public bool IsFinishedWithExam { get; set; }
        public bool SoftDeleted { get; set; }

        //----------
        //relationships

        //TODO Profesor for course optional?
        public int ProfessorID { get; set; }
        public EntityProfessor Professor { get; set; }

        public IList<StudentCourse> Students { get; set; }
    }
}
