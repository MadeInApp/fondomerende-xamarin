using Flurl.Http;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.RESTServices
{
    class LogoutServiceManager
    {
        public string token = Manager.UserManager.Instance.token;
        public async System.Threading.Tasks.Task<LogoutDTO> LogoutAsync()
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                .WithCookie("auth-key", "metticiquellochetipare")
                .WithCookie("token", token)
                .PostUrlEncodedAsync(new { commandName = "logout" })
                .ReceiveJson<LogoutDTO>();

            if (result.response.success = true && result.response.status == 200)
            {
                App.Current.MainPage = new LoginPage();
            }

            return result;
        }
    }
}
