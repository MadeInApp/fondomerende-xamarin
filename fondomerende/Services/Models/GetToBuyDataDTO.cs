using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    class GetToBuyDataDTO
    {
        [JsonProperty("funds-funds-amount")]
        public double funds_funds_amount { get; set; }

        class snacks
        {
            public int id { get; set; }
            public string Friendly_name { get; set; }
            public double price { get; set; }
            [JsonProperty("expiration-in-days")]
            public int expiration_in_days { get; set; }
        }
    }
}
