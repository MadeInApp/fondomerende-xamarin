using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace fondomerende.Services.Models
{
    class SnackDTO
    {
        public ResponseDTO response { get; set; }
        public SnackDataArrayDTO data { get; set; }

        public static implicit operator SnackDTO(HttpResponseMessage v)
        {
            throw new NotImplementedException();
        }
    }
}
