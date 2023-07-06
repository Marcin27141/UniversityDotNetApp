namespace UniversityApi.API.DataBase.Entities
{
    public class EntityProfessor : EntityPerson
    {
        public string IdCode { get; set; }
        public string Subject { get; set; }
        public DateTime FirstDayAtJob { get; set; }
        public int Salary { get; set; }
    }
}
