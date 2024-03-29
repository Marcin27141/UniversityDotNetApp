﻿using ApiDtoLibrary.Person;

namespace UniversityApi.API.DataBase.Entities
{
    public abstract class EntityPerson
    {
        public Guid EntityPersonID { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PESEL { get; set; }
        public DateTime Birthday { get; set; }
        public string Motherland { get; set; }
        public PersonStatus PersonStatus { get; set; }

        public IList<EntityNotification> Notifications { get; set; }
    }
}
