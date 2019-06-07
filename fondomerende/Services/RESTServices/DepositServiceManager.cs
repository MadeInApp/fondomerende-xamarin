using Flurl.Http;
using fondomerende.Manager;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace fondomerende.Services.RESTServices
{
    class DepositServiceManager
    {
        public async System.Threading.Tasks.Task<EatDTO> DepositAsync(double DepAmount) //Servizio per mangiare uno snack

        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
            .WithCookie("auth-key", "metticiquellochetipare")
            .WithCookie("user-token", UserManager.Instance.token)
            .PostUrlEncodedAsync(new { commandName = "deposit", amount = DepAmount})
            .ReceiveJson<EatDTO>();



            return result;
        }
    }
}
