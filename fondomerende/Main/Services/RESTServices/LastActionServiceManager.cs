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
                var response = await "http://fondomerende.madeinapp.net/api/process-request.php?command-name=get-last-actions"
                                    .WithCookie("auth-key", "MEt085D5zxZXK7FES6qMHOrBbuzGPGwBlYzt1cwAJux")
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
                await App.Current.MainPage.DisplayAlert("Fondo Merende", ex.InnerException.Message, "OK");
            }
            return null;
        }
    }
}
