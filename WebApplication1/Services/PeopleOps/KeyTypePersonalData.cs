using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services.People;

namespace WebApplication1.Services.PeopleOps
{
    public class KeyTypePersonalData
    {
        public string Key { get; set; }
        public PersonType Type { get; set; }
        public PersonalData PersonalData { get; set; }
    }

    public enum PersonType
    {
        Student = 1,
        Professor = 2,
    }
}
