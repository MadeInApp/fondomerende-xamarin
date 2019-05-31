using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using fondomerende.PostLoginPages;

namespace fondomerende
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {
        private string username, password;
        private bool remember = false;
        private bool wait = false;
        public LoginPage()
        {
            InitializeComponent();
            Fondo_Merende_logo.Source = ImageSource.FromResource("fondomerende.image.macchinettaNew.png");
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void RememberMeButton_Clicked(object sender, EventArgs e)
        {
            remember = !remember;

            CheckBox.BackgroundColor = remember ? Color.Black : Color.White;
        }

        private async void Bottone_ClickedAsync(object sender, EventArgs e)
        {
            if (!wait)
            {   //assicura che il tasto login venga premuto una volta
                if (!string.IsNullOrEmpty(usernameEntry.Text) && !string.IsNullOrEmpty(passwordEntry.Text))
                {
                    username = usernameEntry.Text;
                    password = passwordEntry.Text;

                    LoginServiceManager loginService = new LoginServiceManager();
                    var response = await loginService.LoginAsync(username, password, remember);

                    if (response.response.success == true)
                    {
                        var page = new MainPage();
                        await Navigation.PushAsync(new MainPage());
                        wait = true;
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
            wait = !wait;
        }

        private async void RegisterButton_ClickedAsync(object sender, EventArgs e)
        {
            if (!wait)
            {
                //App.Current.MainPage = new RegisterPage();
                wait = true;

                await Navigation.PushAsync(new RegisterPage());
            }
            wait = !wait;
        }
    }
}

