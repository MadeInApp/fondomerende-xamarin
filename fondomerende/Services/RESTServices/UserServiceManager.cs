using Flurl.Http;
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
        public async System.Threading.Tasks.Task<UserDTO> AddSnackAsync(string username, string friendly_name, int snackPerBox, int scadenzaGiorni, bool contabile)
        {

            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-user-data"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", token)
                                .GetJsonAsync<UserDTO>();
            return result;

        }
    }
}
