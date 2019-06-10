using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace fondomerende.Services.Models
{
    class AllSnacksDTO
    {
        public ResponseDTO response { get; set; }
        public AllSnackDataArrayDTO data { get; set; }

        public static implicit operator AllSnacksDTO(HttpResponseMessage v)
        {
            throw new NotImplementedException();
        }
    }

}

