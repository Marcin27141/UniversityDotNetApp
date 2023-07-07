using ApiDtoLibrary.Users;

namespace WebApplication1.Services.People
{
    public abstract class Person : IDistinguishableEntity
    {
        public int EntityPersonID { get; set; }
        public AppUserDto AppUser { get; set; }
        public PersonalData PersonalData { get; set; }

        public abstract string Key { get; }

        public int EntityId => EntityPersonID;

        public abstract int EntityClassId { get; }

        public override string ToString() => $"{PersonalData.FirstName} {PersonalData.LastName}";
    }
}
