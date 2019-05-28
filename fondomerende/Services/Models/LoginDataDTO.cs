using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    [JsonObject("data")]
    class LoginDataDTO
    {
        public string token;
    }
}
