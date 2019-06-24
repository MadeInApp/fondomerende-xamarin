using Flurl.Http;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace fondomerende.Main.Services.RESTServices
{
    class LogoutServiceManager
    {
        public string token = Manager.UserManager.Instance.token;
        public async System.Threading.Tasks.Task<LogoutDTO> LogoutAsync()   //Servizio di Log Out
        {
            try
            {
                var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                               .WithCookie("auth-key", "metticiquellochetipare")
                               .WithCookie("token", token)
                               .PostUrlEncodedAsync(new { commandName = "logout" })
                               .ReceiveJson<LogoutDTO>();


                if (result.response.success == true)
                {
                    Preferences.Clear();
                }
                return result;
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
