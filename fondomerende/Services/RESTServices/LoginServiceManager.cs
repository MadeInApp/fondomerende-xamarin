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
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")
                                .PostUrlEncodedAsync(new { commandName = "login", name = username, password = passwordToLogin })
                                .ReceiveJson<LoginDTO>();
            return result;
        }
    }
}
