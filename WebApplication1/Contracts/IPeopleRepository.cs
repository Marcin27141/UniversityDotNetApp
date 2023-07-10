using System;
using System.Collections.Generic;
using WebApplication1.Services.People;

namespace WebApplication1.Contracts
{
    public interface IPeopleRepository
    {
        List<KeyTypePersonalData> GetAllPersonalData();
    }

    public class KeyTypePersonalData
    {
        public Guid Id { get; set; }
        public PersonType Type { get; set; }
        public PersonalData PersonalData { get; set; }
    }

    public enum PersonType
    {
        Student = 1,
        Professor = 2,
    }
}
