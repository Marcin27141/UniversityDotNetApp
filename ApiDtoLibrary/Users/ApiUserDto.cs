﻿using System.ComponentModel.DataAnnotations;

namespace ApiDtoLibrary.Users
{
    public class ApiUserDto : LoginDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
