using Flurl.Http;
using fondomerende.Main.Manager;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace fondomerende.Main.Services.RESTServices
{
    class DepositServiceManager
    {
        public async System.Threading.Tasks.Task<DepositDTO> DepositAsync(Decimal DepAmount)
        {
            DepositDTO result = null;
            try
            {
                    result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                    .WithCookie("auth-key", "metticiquellochetipare")
                    .WithCookie("user-token", UserManager.Instance.token)
                    .WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")
                    .PostUrlEncodedAsync(new { commandName = "deposit", amount = DepAmount })
                    .ReceiveJson<DepositDTO>();

                    return result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", result.response.message, "OK");
            }
            return null;
          
        }
    }
}
