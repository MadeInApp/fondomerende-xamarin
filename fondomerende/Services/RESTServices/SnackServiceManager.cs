using Flurl.Http;
using fondomerende.Manager;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace fondomerende.Services.RESTServices
{
    class SnackServiceManager
    {
        private static string token = UserManager.Instance.token;
        
        public async System.Threading.Tasks.Task<SnackDTO> GetSnacksAsync()
        {
            var result = new SnackDTO();
            try
            {
                result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-snacks-data"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", token)
                                .GetJsonAsync<SnackDTO>();
            }
            catch (FlurlHttpTimeoutException ex)
            {
                Debug.WriteLine(ex);
            }
            catch (FlurlHttpException ex)
            {
                Debug.WriteLine(ex);
            }
            
            return result;
        }

        // iniziato ma devo aspettare l'input dall'utente
        public async System.Threading.Tasks.Task<SnackDTO> GetSnackAsync(String GetSnackName)
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-snack-data&name="
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", token)
                                .PostUrlEncodedAsync(new { commandName = "get-snack-data", name = GetSnackName })
                                .ReceiveJson<SnackDTO>();
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
