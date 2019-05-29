using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    [JsonObject("data")]
    public class SnackDataDTO
    {
        public int id { get; set; }
        public string name { get; set; }

        [JsonProperty("friendly-name")]
        public string friendly_name { get; set; }
        public double price { get; set; }
        public int snack_per_box { get; set; }
        public int expiration_in_days { get; set; }

        
        
    }
}
