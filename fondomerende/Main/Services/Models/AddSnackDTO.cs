using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace fondomerende.Main.Services.Models
{
    public class AddSnackDTO
    {
        public bool success { get; set; }
        public int status { get; set; }
        public string message { get; set; }

        public AddSnackResponseDTO data { get; set; }
    }
}
