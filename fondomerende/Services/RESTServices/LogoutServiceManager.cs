using Flurl.Http;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace fondomerende.Services.RESTServices
{
    class LogoutServiceManager
    {
        public string token = Manager.UserManager.Instance.token;
        public async System.Threading.Tasks.Task<LogoutDTO> LogoutAsync()   //Servizio di Log Out
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                .WithCookie("auth-key", "metticiquellochetipare")
                .WithCookie("token", token)
                .PostUrlEncodedAsync(new { commandName = "logout" })
                .ReceiveJson<LogoutDTO>();
           

           if(result.response.success == true)
           {
                Preferences.Clear();
           }
           return result;
        }
    }
}
