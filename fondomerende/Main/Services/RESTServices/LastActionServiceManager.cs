using Flurl.Http;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.RESTServices
{
    class LastActionServiceManager
    {
        public async System.Threading.Tasks.Task<LastActionDTO> GetLastActions()
        {

            var response = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-last-actions"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", Manager.UserManager.Instance.token)
                                .GetJsonAsync<LastActionDTO>();

            if (response.response.success == true)
            {

            }

            return response;

        }
    }
}
