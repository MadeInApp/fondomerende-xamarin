using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.PostLoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationPage : ContentPage
    {
        public InformationPage()
        {
            InitializeComponent();
        }

        private async void Bottone_ClickedAsync(object sender, EventArgs e)
        {
            LogoutServiceManager loginService = new LogoutServiceManager();
            var response = await loginService.LogoutAsync();

            if (response.response.success == true)
            {
                await Navigation.PopAsync(true);
            }
            else
            {
                await DisplayAlert("Fondo Merende", "Username o Password Errati", "OK");
            }
        }
    }
}