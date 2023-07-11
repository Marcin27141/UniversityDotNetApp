using ApiDtoLibrary.Person;
using ApiDtoLibrary.Users;
using System;

namespace WebApplication1.Services.People
{
    public abstract class Person : IDistinguishableEntity
    {
        public PersonStatus PersonStatus { get; set; }
        public Guid EntityPersonID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public PersonalData PersonalData { get; set; }

        public abstract string Key { get; }

        public Guid EntityId => EntityPersonID;

        public abstract int EntityClassId { get; }

        public override string ToString() => $"{PersonalData.FirstName} {PersonalData.LastName}";
    }
}
