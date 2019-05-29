﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    [JsonObject("data")]
    class SnackDataDTO
    {
        private int id;
        private string name;

        [JsonProperty("friendly-name")]
        private string friendly_name { get; }
        private double price;
        private int snack_per_box;
        private int expiration_in_days;

        
        
    }
}
