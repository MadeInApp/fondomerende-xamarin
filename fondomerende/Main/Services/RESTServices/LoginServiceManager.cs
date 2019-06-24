using Flurl.Http;
using Flurl;
using fondomerende.Main.Manager;
using fondomerende.Main.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using fondomerende.Main.Login.LoginPages;

namespace fondomerende.Main.Services.RESTServices
{
    class LoginServiceManager
    {
        public async System.Threading.Tasks.Task<LoginDTO> LoginAsync(string username, string passwordToLogin, bool remember) //Servizio di Log In
        {
            var data = new Dictionary<string, string>();
            data.Add("commandName", "login");
            data.Add("name", username);
            data.Add("password", passwordToLogin);

            try
            {
               var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                    .WithCookie("auth-key", "metticiquellochetipare")
                    .WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")
                    .PostUrlEncodedAsync(data)
                    .ReceiveJson<LoginDTO>();

                if (result.response.success == true)
                {
                    UserManager.Instance.token = result.data.token;
                    Preferences.Set("username", username);
                    Preferences.Set("password", passwordToLogin);
                    Preferences.Set("Logged", remember);
                    Preferences.Set("token", result.data.token);
                }
                return result;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Connessione al server scaduta", "OK");
            }
            catch (FlurlHttpException ex)
            {
                await App.Current.MainPage.DisplayAlert("Login", "Errore", "OK");
            }
            return null;
        }

    }
}
