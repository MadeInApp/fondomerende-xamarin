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
            LoggedAs.Text = "Loggato come: " + Preferences.Get("username", null);
            LoggedAs.Opacity = 0.5;


        }

        private bool DevicePlatform(bool v1, bool v2, bool v3)
        {
            throw new NotImplementedException();
        }

        private async void LogOut_button_Clicked(object sender, EventArgs e)
        {
            LogoutServiceManager logoutService = new LogoutServiceManager();
            var response = await logoutService.LogoutAsync();

            if (response.response.success == true)
            {
                Navigation.RemovePage(this);
                Preferences.Clear("username");
                Preferences.Clear("password");
                Preferences.Clear("token");
                Preferences.Clear("Logged");

            }
            else
            {
                await DisplayAlert("Fondo Merende", "Guarda, sarò franco. sta cosa non ha senso", "OK");
            }
        }
    }
}