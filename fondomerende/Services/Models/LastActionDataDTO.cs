using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{

    public class LastActionDataDTO
    {
        string[] contenuto { get; set; }
    }

    [JsonObject("actions")]
    public class LastActionDataArrayDTO
    {
        public LastActionDataDTO actions { get; set; }
    }

}
