using Flurl.Http;
using fondomerende.Manager;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;

namespace fondomerende.Services.RESTServices
{
    class SnackServiceManager
    {

        private static string token = UserManager.Instance.token;

        public async System.Threading.Tasks.Task<SnackDTO> GetSnacksAsync() //  Servizio per Ottenere informazioni sugli snack mangiabili
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-to-eat-and-user-funds"
                            .WithCookie("auth-key", "metticiquellochetipare")
                            .WithCookie("user-token", token)
                            .GetJsonAsync<SnackDTO>();
            return result;
        }

        // iniziato ma devo aspettare l'input dall'utente
        public async System.Threading.Tasks.Task<SnackDTO> GetSnackAsync(string GetSnackName)  // Servizio per Ottenere informazioni su uno snack in particolare (non usato per ora)
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-snack-data&name="
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", token)
                                .PostUrlEncodedAsync(new { commandName = "get-snack-data", name = GetSnackName })
                                .ReceiveJson<SnackDTO>();
            return result;
        
        }

        public async System.Threading.Tasks.Task<SnackDTO> AddSnackAsync(string nome, double prezzo, int snackPerBox, int scadenzaGiorni, bool contabile) //Servizio per aggiungere snacks (non usato per ora)
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", token)
                                .PostUrlEncodedAsync(new { commandName = "add-snack", name = nome, price = prezzo, snack_per_box = snackPerBox, expiration_in_days = scadenzaGiorni, countable = contabile } )
                                .ReceiveJson<SnackDTO>();
            return result;

        }

        public async System.Threading.Tasks.Task<EatDTO> EatAsync(int idsnack, int quantity_snack) //Servizio per mangiare uno snack

        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
            .WithCookie("auth-key", "metticiquellochetipare")
            .WithCookie("user-token", token)
            .PostUrlEncodedAsync(new { commandName = "eat", id = idsnack, quantity = quantity_snack })
            .ReceiveJson<EatDTO>();



            return result;
        }

    }
}
