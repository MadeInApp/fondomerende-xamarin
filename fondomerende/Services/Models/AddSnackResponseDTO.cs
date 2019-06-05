using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    [JsonObject("response")]
    public class AddSnackResponseDTO
    {
        public bool success { get; set; }
        public int status { get; set; }
        public AddSnackDataDTO data { get; set; }
        public string message { get; set; }
    }
}
