using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    [JsonObject("actions")]
    public class LastActionDataDTO
    {
        public string[] actions { get; set; }
    }

}
