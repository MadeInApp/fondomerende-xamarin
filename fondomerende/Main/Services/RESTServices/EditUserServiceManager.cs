using System;
using System.Collections.Generic;
using System.Text;
using Flurl.Http;
using fondomerende.Main.Services.Models;

namespace fondomerende.Main.Services.RESTServices
{
    class EditUserServiceManager
    {

        public async System.Threading.Tasks.Task<EditUserDTO> EditUserAsync(string ChangeUsername, string ChangeFriendlyName, string ChangePassword)
        {
            var data = new Dictionary<string, string>();
            {
                data.Add("commandName", "edit-user");
                data.Add("name", ChangeUsername);
                data.Add("friendly-name", ChangeFriendlyName);
                data.Add("password", ChangePassword);
            }
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                .WithCookie("auth-key", "metticiquellochetipare")
                .WithCookie("token", Manager.UserManager.Instance.token)
                .PostUrlEncodedAsync(data)
                .ReceiveJson<EditUserDTO>();

            return result;
        }

    }
}
