namespace UniversityApi.API.DataBase.Entities
{
    public class EntityNotification
    {
        public Guid EntityNotificationId { get; set; }
        public EntityPerson Recipient { get; set; }
        public Guid RecipientId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsNew { get; set; }
    }
}
