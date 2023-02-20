using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DataBase.Entities
{
    public class EntityStudent
    {
        public int EntityStudentID { get; set; }
        public string StudentIndex { get; set; }
        public DateTime BeginningOfStudying { get; set; }
        public bool SoftDeleted { get; set; }

        //----------------
        //relationships
        public int PersonalDataID { get; set; }
        public EntityPersonalData PersonalData { get; set; }
        public IList<StudentCourse> Courses { get; set; }
    }
}
