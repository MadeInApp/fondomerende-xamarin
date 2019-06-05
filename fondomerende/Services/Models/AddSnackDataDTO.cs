using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    [JsonObject("data")]
    public class AddSnackDataDTO
    {
        [JsonProperty("snack-id")]
        public int id { get; set; }
    }
}
