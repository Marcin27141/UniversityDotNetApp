﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtoLibrary.Students
{
    public class BaseGetStudent : BaseStudent
    {
        public Guid EntityPersonId { get; set; }
    }
}
