using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtoLibrary.Professors
{
    public class BaseGetProfessor : BaseProfessor
    {
        public Guid EntityPersonId { get; set; }
    }
}
