using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace fondomerende
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {
        private string username, password;
        public LoginPage()
        {
            InitializeComponent();
            macchinetta_immagine.Source = ImageSource.FromResource("fondomerende.image.macchinetta_merende.png");
        }

        private async void Bottone_ClickedAsync(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(usernameEntry.Text) && !string.IsNullOrEmpty(passwordEntry.Text))
            {
                username = usernameEntry.Text;
                password = passwordEntry.Text;
                LoginServiceManager loginService = new LoginServiceManager();
                var response = await loginService.LoginAsync(username, password);

            }
            else
            {
                await DisplayAlert("Fondo Merende", "Username o Password mancanti", "OK");
            }
        }
    }
}

