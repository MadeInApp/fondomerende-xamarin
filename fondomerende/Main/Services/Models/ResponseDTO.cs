﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    [JsonObject("response")]
    class ResponseDTO
    {

        public bool success { get; set; }
        public int status { get; set; }
        public string message { get; set; }
    }
}
