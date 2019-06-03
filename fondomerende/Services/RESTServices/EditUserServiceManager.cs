using System;
using System.Collections.Generic;
using System.Text;
using Flurl.Http;
using fondomerende.Services.Models;

namespace fondomerende.Services.RESTServices
{
    class EditUserServiceManager
    {

        public async System.Threading.Tasks.Task<EditUserDTO> EditUserAsync(string ChangeFriendlyName, string ChangeUsername, string ChangePassword)
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                .WithCookie("auth-key", "metticiquellochetipare")
                .WithCookie("token", Manager.UserManager.Instance.token)
                .PostUrlEncodedAsync(new { commandName = "edit-user", name = ChangeUsername, friendly_name = ChangeFriendlyName, password = ChangePassword })
                .ReceiveJson<EditUserDTO>();

            return result;
        }

    }
}
