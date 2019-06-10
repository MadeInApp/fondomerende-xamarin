using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    class UserFundsDTO
    {
        public ResponseDTO response { get; set; }

        public UserFundsDataDTO data { get; set; }
    }
}
