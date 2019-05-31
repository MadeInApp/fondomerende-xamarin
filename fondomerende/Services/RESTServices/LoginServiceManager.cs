using Flurl.Http;
using fondomerende.Manager;
using fondomerende.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace fondomerende.Services.RESTServices
{
    class LoginServiceManager
    {

        public async System.Threading.Tasks.Task<LoginDTO> LoginAsync(string username, string passwordToLogin, bool remember) //Servizio di Log In
        {
            var result = await "http://192.168.0.175:8888/fondomerende/public/process-request.php"
                .WithCookie("auth-key", "metticiquellochetipare")
                .WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")
                .PostUrlEncodedAsync(new { commandName = "login", name = username, password = passwordToLogin })
                .ReceiveJson<LoginDTO>();

            if (result.response.success = true && result.response.status == 201)
            {
                UserManager.Instance.token = result.data.token;
                Preferences.Set("username", username);
                Preferences.Set("password", passwordToLogin);
                Preferences.Set("Logged", remember);
                Preferences.Set("token", result.data.token);
            }
            else
            {
                await DisplayAlert("Fondo Merende", "Username o Password Errati", "OK");
            }

            return result;
        }  
    }
}
