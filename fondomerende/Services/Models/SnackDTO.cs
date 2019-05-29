using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.Models
{
    class SnackDTO
    {
        public ResponseDTO response { get; set; }
        public List<SnackDataDTO> data { get; set; }
    }
}
