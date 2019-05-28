using Flurl.Http;
using fondomerende.Manager;
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
            var result = await Services.LoginUrlRequest()
                                .PostUrlEncodedAsync(new { commandName = "login", name = username, password = passwordToLogin })
                                .ReceiveJson<LoginDTO>();
            if (result.response.success = true && result.response.status == 201)
            {
                UserManager.Instance.token = result.data.token;
            }

            return result;
        }  
    }
}
