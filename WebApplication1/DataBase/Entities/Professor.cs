using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DataBase.Entities
{
    public class Professor
    {
        public int ProfessorID { get; set; }
        public string IdCode { get; set; }
        public string Subject { get; set; }
        public DateTime FirstDayAtJob { get; set; }
        public int Salary { get; set; }
        public bool SoftDeleted { get; set; }

        //----------------
        //relationships
        public int PersonalDataID { get; set; }
        public PersonalData PersonalData { get; set; }
    }
}
