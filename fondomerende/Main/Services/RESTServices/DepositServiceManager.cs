using Flurl.Http;
using fondomerende.Main.Manager;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace fondomerende.Main.Services.RESTServices
{
    class DepositServiceManager
    {
        public async System.Threading.Tasks.Task<DepositDTO> DepositAsync(float DepAmount)

        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
            .WithCookie("auth-key", "metticiquellochetipare")
            .WithCookie("user-token", UserManager.Instance.token)
            .WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")
            .PostUrlEncodedAsync(new { commandName = "deposit", amount = DepAmount})
            .ReceiveJson<DepositDTO>();

            return result;
        }
    }
}
