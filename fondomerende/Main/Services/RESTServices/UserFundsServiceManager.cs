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
            string urlString = Services.Concatenazione() + "/process-request.php?command-name=get-user-funds";
            try
            {
            var response = await urlString
                    .WithCookie("auth-key", "MEt085D5zxZXK7FES6qMHOrBbuzGPGwBlYzt1cwAJux")
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
                if(ex.InnerException.Message != "Collection was modified; enumeration operation may not execute.")
                {
                    await App.Current.MainPage.DisplayAlert("Get User Funds", ex.InnerException.Message, "OK");
                }
            }
            return null;
        }

        public async System.Threading.Tasks.Task<FundFundsDTO> GetFundsFundAsync()
        {
            string urlString = Services.Concatenazione() + "/process-request.php?command-name=get-fund-funds";
            try
            {
                var response = await urlString
                                    .WithCookie("auth-key", "MEt085D5zxZXK7FES6qMHOrBbuzGPGwBlYzt1cwAJux")
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
                await App.Current.MainPage.DisplayAlert("Fondo Merende", ex.InnerException.Message, "OK");
            }
            return null;
        }

    }
}
