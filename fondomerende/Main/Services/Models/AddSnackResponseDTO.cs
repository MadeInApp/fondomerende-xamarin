using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    
    public class AddSnackResponseDTO
    {
        [JsonObject("data")]
        public class AddSnackDataDTO
        {
            [JsonProperty("snack-id")]
            public int id { get; set; }
        }
        public bool success { get; set; }
        public int status { get; set; }
        public string message { get; set; }
        public AddSnackDataDTO data { get; set; }
    }
}
