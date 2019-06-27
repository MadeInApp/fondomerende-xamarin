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
            string urlString = Services.Concatenazione() + "?command-name=deposit";
			try
			{
				var data = new Dictionary<string, string>();
				{
					data.Add("command-name", "deposit");
					data.Add("amount", Convert.ToString(DepAmount));
				}

                var result = await Services.Concatenazione()
                    .WithCookie("auth-key", Services.GetAuthKey())
                    .WithCookie("user-token", UserManager.Instance.token)
                    .WithHeader("Content-Type", "application/x-www-form-urlencoded; param=value;charset=UTF-8")
                    .PostUrlEncodedAsync(new { amount = DepAmount})
                    .ReceiveJson<DepositDTO>();

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
