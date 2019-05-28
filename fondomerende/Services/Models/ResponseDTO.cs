using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    [JsonObject("response")]
    class ResponseDTO
    {

        public bool success;
        public int status;
        public string message;
    }
}
