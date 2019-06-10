using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace fondomerende.Main.Services.Models
{
    class ToBuySnackDTO
    {
        public ResponseDTO response { get; set; }

        public ToBuyDataArrayDTO data { get; set; }


        public static implicit operator ToBuySnackDTO(HttpResponseMessage v)
        {
            throw new NotImplementedException();
        }
    }
}

