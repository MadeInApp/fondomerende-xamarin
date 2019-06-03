﻿using System;
using System.Collections.Generic;
using System.Text;
using fondomerende.Services.Models;
using Flurl.Http;

namespace fondomerende.Services.RESTServices
{
    class UserFundsServiceMangaer
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