using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public EntityCourse OnCourseProfessorAssignment([EventMessage] EntityCourse course, [Service] UniversityApiDbContext dbContext)
        {
            var recipient = dbContext.People.Include(p => p.Notifications).SingleOrDefault(p => p.EntityPersonID == course.ProfessorId.Value);
            if (recipient != null)
            {
                var notification = new EntityNotification
                {
                    RecipientId = course.ProfessorId.Value,
                    Title = "New course assigned",
                    Body = $"You have been assigned to a course {course.CourseCode} {course.Name}",
                    IsNew = true
                };
                recipient.Notifications.Add(notification);
                dbContext.SaveChanges();
            }

            return course;
        }
    }
}
