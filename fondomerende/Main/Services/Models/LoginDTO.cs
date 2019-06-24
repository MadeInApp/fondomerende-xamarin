using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace fondomerende.Main.Services.Models
{
    class LoginDTO
    {
        public ResponseDTO response { get; set; }
        public LoginDataDTO data { get; set; }
    }
}
