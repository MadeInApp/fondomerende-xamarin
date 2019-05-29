using Flurl.Http;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.RESTServices
{
    class RegisterServiceManager
    {
        public async System.Threading.Tasks.Task<RegisterDTO> RegisterAsync(string username, string passwordToLogin, string friendly_name, int admin2)
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                .WithCookie("auth-key", "metticiquellochetipare")
                .WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")
                .PostUrlEncodedAsync(new { commandName = "add-user", name = username, friendlyname = friendly_name, password = passwordToLogin, admin = admin2})
                .ReceiveJson<RegisterDTO>();

            /*if (result.response.success = true && result.response.status == 201)
            {
                UserManager.Instance.token = result.data.token;
                Preferences.Set("username", username);
                Preferences.Set("password", passwordToLogin);
                Preferences.Set("Logged", remember);
            }*/

            return result;
        }
    }
}
