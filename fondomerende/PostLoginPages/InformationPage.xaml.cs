using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace fondomerende.PostLoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationPage : ContentPage
    {
        public InformationPage()
        {
            InitializeComponent();
            LoggedAs.Text = "Loggato Come: " + Preferences.Get("username", null);
        }

        private async void LogOut_ClickedAsync(object sender, EventArgs e)
        {
            LogoutServiceManager loginService = new LogoutServiceManager();
            var response = await loginService.LogoutAsync();

            if (response.response.success == true)
            {
                App.Current.MainPage = new LoginPage();
            }
            else
            {
                await DisplayAlert("Fondo Merende", "probabilmente il server è off sry", "OK");
            }
        }
    }
}