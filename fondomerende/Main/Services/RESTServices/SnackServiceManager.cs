using Flurl.Http;
using fondomerende.Main.Manager;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace fondomerende.Main.Services.RESTServices
{
    class SnackServiceManager
    {
        public async System.Threading.Tasks.Task<SnackDTO> GetSnacksAsync() //  Servizio per Ottenere informazioni sugli snack mangiabili
        {
            try
            {
                var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-to-eat-and-user-funds"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", UserManager.Instance.token)
                                .GetJsonAsync<SnackDTO>();
                return result;
            }

            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Errore di rete", "OK");
            }
            return null;
        }

        public async System.Threading.Tasks.Task<ToBuySnackDTO> GetToBuySnacksAsync() //  Servizio per Ottenere informazioni sugli snack mangiabili
        {
            try
            {
                var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-to-buy-and-fund-funds"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", UserManager.Instance.token)
                                .GetJsonAsync<ToBuySnackDTO>();
                return result;
            }

            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Errore di rete", "OK");
            }
            return null;
        }

        public async System.Threading.Tasks.Task<AllSnacksDTO> GetAllSnacksAsync() //  Servizio per Ottenere informazioni sugli snack mangiabili
        {
            try
            {
                var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-snacks-data"
                                .WithCookie("auth-key", "metticiquellochetipare")
                                .WithCookie("user-token", UserManager.Instance.token)
                                .GetJsonAsync<AllSnacksDTO>();
                return result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Errore di rete", "OK");
            }
            return null;
        }


        // iniziato ma devo aspettare l'input dall'utente
        public async System.Threading.Tasks.Task<GetSnackDTO> GetSnackAsync(string GetSnackName)  // Servizio per Ottenere informazioni su uno snack in particolare (non usato per ora)
        {
            string lol = "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-snack-data&name=" + GetSnackName;
            try
            {
                var result = await lol
                                    .WithCookie("auth-key", "metticiquellochetipare")
                                    .WithCookie("user-token", UserManager.Instance.token)
                                    .GetJsonAsync<GetSnackDTO>();
                return result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Errore di rete", "OK");
            }
            return null;

        }

        public async System.Threading.Tasks.Task<AddSnackDTO> AddSnackAsync(string nome, Double prezzo, int snackPerBox, int scadenzaGiorni, bool contabile) //Servizio per aggiungere snacks (non usato per ora)
        {
            var data = new Dictionary<string, string>();
            data.Add("commandName", "add-snack");
            data.Add("name", nome);
            data.Add("price", Convert.ToString(prezzo));
            data.Add("snacks-per-box", Convert.ToString(snackPerBox));
            data.Add("expiration-in-days", Convert.ToString(scadenzaGiorni));
            data.Add("countable", Convert.ToString(contabile));

            try
            {
                var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                                    .WithCookie("auth-key", "metticiquellochetipare")
                                    .WithCookie("user-token", UserManager.Instance.token)
                                    .PostUrlEncodedAsync(data)
                                    .ReceiveJson<AddSnackDTO>();
                return result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Errore di rete", "OK");
            }
            return null;
        }

        internal Task BuySnackAsync(object selectedSnackID, int v)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task<EatDTO> EatAsync(int idsnack, int quantity_snack) //Servizio per mangiare uno snack

        {
            try
            {
                var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                .WithCookie("auth-key", "metticiquellochetipare")
                .WithCookie("user-token", UserManager.Instance.token)
                .PostUrlEncodedAsync(new { commandName = "eat", id = idsnack, quantity = quantity_snack })
                .ReceiveJson<EatDTO>();

                return result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Errore di rete", "OK");
            }
            return null;
        }

        public async System.Threading.Tasks.Task<EditSnackDTO> EditSnackAsync(int idSnack, string snackName, string snackPrice, string snacksPerBox, string SnackExpiration, int Qta) //Servizio per mangiare uno snack

        {
            var data = new Dictionary<string, string>();
            data.Add("commandName", "edit-snack");
            data.Add("id", Convert.ToString(idSnack));
            data.Add("name", snackName);
            data.Add("price",snackPrice.Replace(",", "."));
            data.Add("snacks-per-box", snacksPerBox);
            data.Add("expiration-in-days", SnackExpiration);
            data.Add("quantity", Convert.ToString(Qta));
            try
            {
                var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                .WithCookie("auth-key", "metticiquellochetipare")
                .WithCookie("user-token", UserManager.Instance.token)
                .PostUrlEncodedAsync(data)
                .ReceiveJson<EditSnackDTO>();

                return result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Errore di rete", "OK");
            }
            return null;
        }

        public async System.Threading.Tasks.Task<EatDTO> BuySnackAsync(int idsnack, int quantity)
        {
            try
            {
                var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                .WithCookie("auth-key", "metticiquellochetipare")
                .WithCookie("user-token", UserManager.Instance.token)
                .PostUrlEncodedAsync(new { commandName = "buy", id = idsnack, quantity = quantity })
                .ReceiveJson<EatDTO>(); //ri uso il DTO di Eat perchè sono Lazyaf

                return result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Errore di rete", "OK");
            }
            return null;
        }


    }
}
