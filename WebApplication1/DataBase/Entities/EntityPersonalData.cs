using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebApplication1.Pages.ShowResults.ShowPeopleModel;

namespace WebApplication1.DataBase.Entities
{
    public class EntityPersonalData
    {
        public ApplicationUser ApplicationUser { get; set; }
        public int EntityPersonalDataID { get; set; }
        public int PersonTypeID { get; set; }
        public string SpecificId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PESEL { get; set; }
        public DateTime Birthday { get; set; }
        public string Motherland { get; set; }
        public bool SoftDeleted { get; set; }
    }
}
