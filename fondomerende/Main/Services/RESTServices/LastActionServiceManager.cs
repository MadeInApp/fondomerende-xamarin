using Flurl.Http;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Services.RESTServices
{
    class LastActionServiceManager
    {
        public async System.Threading.Tasks.Task<LastActionDTO> GetLastActions()
        {
            try
            {
                var response = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-last-actions"
                                    .WithCookie("auth-key", "metticiquellochetipare")
                                    .WithCookie("user-token", Manager.UserManager.Instance.token)
                                    .GetJsonAsync<LastActionDTO>();
                return response;
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
