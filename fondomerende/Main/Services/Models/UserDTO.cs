﻿using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    class UserDTO
    {
        public bool success { get; set; }
        public int status { get; set; }
        public string message { get; set; }

        public UserDataObjectDTO data { get; set; }
    }
}
