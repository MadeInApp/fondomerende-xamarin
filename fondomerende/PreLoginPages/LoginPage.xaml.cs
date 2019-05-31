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
        bool clicked = false;
        UserServiceManager userService;

        public LoginPage()
        {
            InitializeComponent();
            Fondo_Merende_logo.Source = ImageSource.FromResource("fondomerende.image.macchinettaNew.png");
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void RememberMeButton_Clicked(object sender, EventArgs e) //Ricorda nome utente e pw (da fixare)
        {
            if (clicked == true)
            {
                clicked = false;
                remember = !remember;
                RememberMe_Button.BackgroundColor = Color.Transparent;   
            }
            else
            {
                clicked = true;
                remember = !remember;
                RememberMe_Button.BackgroundColor = Color.WhiteSmoke;
            }

        }

        private async void Bottone_ClickedAsync(object sender, EventArgs e) //Effettua il Log In
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
                        userService = new UserServiceManager();
                        await userService.GetUserData();   //informazioni utente
                        App.Current.MainPage = new MainPage();                                       
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

        private async void RegisterButton_ClickedAsync(object sender, EventArgs e) //Mostra il form di registrazione
        {
            if (!wait)
            {
                wait = true;
                await Navigation.PushAsync(new RegisterPage());
            }
            wait = !wait;
        }

        
    }
}

