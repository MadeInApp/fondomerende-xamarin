using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    class LoginDTO
    {
        public ResponseDTO response;
        public LoginDataDTO data { get; set; }
    }
}
