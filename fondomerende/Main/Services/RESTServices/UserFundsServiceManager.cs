using System;
using System.Collections.Generic;
using System.Text;
using fondomerende.Main.Services.Models;
using Flurl.Http;
using Xamarin.Essentials;

namespace fondomerende.Main.Services.RESTServices
{

    class UserFundsServiceManager
    {

       
        public async System.Threading.Tasks.Task<UserFundsDTO> GetUserFunds()
        {
            var response = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-user-funds"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", Manager.UserManager.Instance.token)
                                .GetJsonAsync<UserFundsDTO>();

            return response;
        }
    }
}
