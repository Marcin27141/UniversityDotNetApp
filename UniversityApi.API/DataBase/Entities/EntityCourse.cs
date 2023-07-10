namespace UniversityApi.API.DataBase.Entities
{
    public class EntityCourse
    {
        public Guid EntityCourseID { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public int ECTS { get; set; }
        public bool IsFinishedWithExam { get; set; }
        public bool SoftDeleted { get; set; }

        //----------
        //relationships
#nullable enable
        public int? ProfessorID { get; set; }
        public EntityProfessor? Professor { get; set; }
#nullable disable

        public IList<EntityStudent> Students { get; set; }
    }
}
