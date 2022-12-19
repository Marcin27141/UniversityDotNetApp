using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DataBase.Entities
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentIndex { get; set; }
        public float Average { get; set; }
        public DateTime BeginningOfStudying { get; set; }
        public bool SoftDeleted { get; set; }

        //----------------
        //relationships
        public int PersonalDataID { get; set; }
        public PersonalData PersonalData { get; set; }
        public IList<StudentCourse> Courses { get; set; }
    }
}
