using Flurl.Http;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.RESTServices
{
    class SnackServiceManager
    {
        public async System.Threading.Tasks.Task<SnackDTO> SnackAsync()
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-snacks-data"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", "98588a8c5f6223a3461044bd")
                                /*.WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")*/
                                .GetJsonAsync<SnackDTO>();
            return result;
        }
    }
}
