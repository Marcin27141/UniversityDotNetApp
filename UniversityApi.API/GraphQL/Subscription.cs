using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public EntityCourse OnCourseProfessorAssignment([EventMessage] EntityCourse course) => course;
    }
}
