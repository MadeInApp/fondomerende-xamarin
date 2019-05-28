using Flurl.Http;
using fondomerende.Manager;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Services.RESTServices
{
    class SnackServiceManager
    {
        private static string token = UserManager.Instance.token;
        
        public async System.Threading.Tasks.Task<SnackDTO> GetSnacksAsync()
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-snacks-data"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", token)
                                /*.WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")*/
                                .GetJsonAsync<SnackDTO>();
            return result;
        }

        // iniziato ma devo aspettare l'input dall'utente
        public async System.Threading.Tasks.Task<SnackDTO> GetSnackAsync(String nome)
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-snack-data&name="
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", token)
                                .GetJsonAsync<SnackDTO>();
            return result;
        
        }

        public async System.Threading.Tasks.Task<SnackDTO> AddSnackAsync(String nome, double prezzo, int snackPerBox, int scadenzaGiorni, int contabile)
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", token)
                                .PostUrlEncodedAsync(new { commandName = "add-snack", name = nome, price = prezzo, snack_per_box = snackPerBox, expiration_in_days = scadenzaGiorni, countable = contabile } )
                                .ReceiveJson<SnackDTO>();
            return result;

        }
    }
}
