using Newtonsoft.Json;
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
        private string friendly_name;
        private double price;
        private int snack_per_box;
        private int expiration_in_days;

    }
}
