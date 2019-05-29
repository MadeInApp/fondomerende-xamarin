using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace fondomerende
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {
        private string username, password;
        private bool remember;
        public LoginPage()
        {
            InitializeComponent();

            macchinetta_immagine.Source = ImageSource.FromResource("fondomerende.image.Fondo_Merende_logo.png");
        }

        private void RememberMeButton_Clicked(object sender, EventArgs e)
        {
            remember = !remember;

            CheckBox.BackgroundColor = remember ? Color.Black : Color.White;
        }

        private async void Bottone_ClickedAsync(object sender, EventArgs e)
        {
            SnackServiceManager snackService = new SnackServiceManager();
            var a = await snackService.GetSnacksAsync();
            if (!string.IsNullOrEmpty(usernameEntry.Text) && !string.IsNullOrEmpty(passwordEntry.Text))
            {
                username = usernameEntry.Text;
                password = passwordEntry.Text;
                LoginServiceManager loginService = new LoginServiceManager();
                var response = await loginService.LoginAsync(username, password, remember);
                if(response.response.success == true)
                {
                    App.Current.MainPage = new MainPage();
                }
                else
                {
                    await DisplayAlert("Fondo Merende", "Username o Password Errati", "OK");
                }

            }
            else
            {
                await DisplayAlert("Fondo Merende", "Username o Password mancanti", "OK");
            }
        }

        private async void RegisterButton_ClickedAsync(object sender, EventArgs e)
        {

        }
    }
}

