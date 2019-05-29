using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    [JsonObject("data")]
    class SnackDataDTO
    {
        private int id { get; set; }
        private string name { get; set; }

        [JsonProperty("friendly-name")]
        private string friendly_name { get; set; }
        private double price { get; set; }

        [JsonProperty("snack-per-box")]
        private int snack_per_box { get; set; }

        [JsonProperty("expiration-in-days")]
        private int expiration_in_days { get; set; }



    }
}
