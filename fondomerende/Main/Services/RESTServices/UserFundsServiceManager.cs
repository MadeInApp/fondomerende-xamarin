using System;
using System.Collections.Generic;
using System.Text;
using fondomerende.Main.Services.Models;
using Flurl.Http;
using Xamarin.Essentials;

namespace fondomerende.Main.Services.RESTServices
{

    class UserFundsServiceManager
    {

       
        public async System.Threading.Tasks.Task<UserFundsDTO> GetUserFunds()
        {
            try
            {


              var  response = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-user-funds"
                                    .WithCookie("auth-key", "metticiquellochetipare")
                                    .WithCookie("user-token", Manager.UserManager.Instance.token)
                                    .GetJsonAsync<UserFundsDTO>();

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

        public async System.Threading.Tasks.Task<FundFundsDTO> GetFundsFundAsync()
        {
            try
            {
                var response = await "http://192.168.0.175:8888/fondomerende/public/process-request.php?commandName=get-fund-funds"
                                    .WithCookie("auth-key", "metticiquellochetipare")
                                    .WithCookie("user-token", Manager.UserManager.Instance.token)
                                    .GetJsonAsync<FundFundsDTO>();

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
