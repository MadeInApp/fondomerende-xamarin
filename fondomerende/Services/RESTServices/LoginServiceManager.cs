using Flurl.Http;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.RESTServices
{
    class LoginServiceManager
    {

        public async System.Threading.Tasks.Task<LoginDTO> LoginAsync(string username, string passwordToLogin)
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                                .WithCookie("Cookie", "auth-key=metticiquellochetipare")
                                .PostJsonAsync(new { commandName = "login", name = username, password = passwordToLogin })
                                .ReceiveJson<LoginDTO>();
            return result;
        }
    }
}
