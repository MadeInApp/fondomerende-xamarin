﻿using Flurl.Http;
using fondomerende.Manager;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace fondomerende.Services.RESTServices
{
    class UserServiceManager
    {
        public string token = Manager.UserManager.Instance.token;

        //Servizio per ricevere i dati utente
        public async System.Threading.Tasks.Task<UserDTO> GetUserData()
        {

            var response = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-user-data"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", token)
                                .GetJsonAsync<UserDTO>();

            if (response.response.success == true)
            {
                Preferences.Set("friendly-name", response.data.userList.friendly_name);
            }
            return response;

        }
    }
}