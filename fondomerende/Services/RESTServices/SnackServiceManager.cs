using Flurl.Http;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.RESTServices
{
    class SnackServiceManager
    {
        public async System.Threading.Tasks.Task<SnackDTO> SnackAsync()
        {
            var result = await Services.LoginUrlRequest()
                                .PostUrlEncodedAsync(new { commandName = "get-snacks-data" })
                                .ReceiveJson<SnackDTO>();
            return result;
        }
    }
}
