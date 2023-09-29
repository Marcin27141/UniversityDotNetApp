namespace UniversityApi.API.DataBase.Entities
{
    public class EntityCourse
    {
        public Guid EntityCourseID { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public int ECTS { get; set; }
        public bool IsFinishedWithExam { get; set; }

        //----------
        //relationships
#nullable enable
        public EntityProfessor? Professor { get; set; }
        public Guid? ProfessorId { get; set; }
#nullable disable

        public IList<EntityStudent> Students { get; set; }
    }
}
