using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    class FundFundsDTO
    {
        public ResponseDTO response { get; set; }

        public FundFundsDataDTO data { get; set; }
    }
}
