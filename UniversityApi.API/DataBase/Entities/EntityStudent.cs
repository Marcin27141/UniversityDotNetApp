namespace UniversityApi.API.DataBase.Entities
{
    public class EntityStudent : EntityPerson
    {
        public string Index { get; set; }
        public DateTime BeginningOfStudying { get; set; }

        //----------------
        //relationships
        public IList<EntityCourse> Courses { get; set; }
    }
}
