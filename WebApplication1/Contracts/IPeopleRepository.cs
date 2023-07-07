using System.Collections.Generic;
using WebApplication1.Services.People;
using WebApplication1.Services.PeopleOps;

namespace WebApplication1.Contracts
{
    public interface IPeopleRepository
    {
        List<KeyTypePersonalData> GetAllPersonalData();
    }

    public class KeyTypePersonalData
    {
        public int Id { get; set; }
        public PersonType Type { get; set; }
        public PersonalData PersonalData { get; set; }
    }

    public enum PersonType
    {
        Student = 1,
        Professor = 2,
    }
}
