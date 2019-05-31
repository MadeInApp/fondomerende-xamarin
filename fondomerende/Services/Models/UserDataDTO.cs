using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    public class UserDataDTO
    { 
        public string name { get; set; }

        [JsonProperty("friendly-name")]
        public string friendly_name { get; set; }

    }

    [JsonObject("user")]
    public class UserDataArrayDTO
    {
        public List<UserDataDTO> user { get; set; }
    }
}
