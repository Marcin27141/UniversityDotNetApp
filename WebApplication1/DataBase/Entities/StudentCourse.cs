using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DataBase.Entities
{
    public class StudentCourse
    {
        public int StudentID { get; set; }
        public EntityStudent Student { get; set; }
        public int CourseID { get; set; }
        public EntityCourse Course { get; set; }
    }
}
