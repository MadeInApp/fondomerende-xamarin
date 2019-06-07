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
        private string username, password, testpassword, friendly_name;
        private bool remember = false;
        private bool wait = false;
        bool clicked = false;
        UserServiceManager userService;

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Donut_Background();
            LoginFade();
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

        private async void Login_ClickedAsync(object sender, EventArgs e) //Effettua il Log In
        {
            if (!wait)
            {   //assicura che il tasto login venga premuto una volta
                if (!string.IsNullOrEmpty(usernameEntry.Text) && !string.IsNullOrEmpty(passwordEntry.Text))
                {
                    username = usernameEntry.Text;
                    password = passwordEntry.Text;

                    LoginServiceManager loginService = new LoginServiceManager();
                    var response = await loginService.LoginAsync(username, password, !remember);

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
            await LoginStack.FadeTo(0, 500);
            await LoginStack.TranslateTo(0, 1000, 1);
            await RegisterStack.TranslateTo(0, 0, 1);
            await RegisterStack.FadeTo(1, 500);
        }

   

        private async void Register_ClickedAsync(object sender, EventArgs e)
        {
            //SnackServiceManager snackService = new SnackServiceManager();
            //var a = await snackService.GetSnacksAsync();
            if (!string.IsNullOrEmpty(usernameEntryR.Text) && !string.IsNullOrEmpty(friendlyNameEntryR.Text) && !string.IsNullOrEmpty(passwordEntryR.Text) && !string.IsNullOrEmpty(testPasswordEntryR.Text))
            {
                password = passwordEntryR.Text;
                testpassword = testPasswordEntryR.Text;
                username = usernameEntryR.Text;
                friendly_name = friendlyNameEntryR.Text;

                if (password == testpassword)
                {

                    RegisterServiceManager registerService = new RegisterServiceManager();
                    var response = await registerService.RegisterAsync(username, password, friendly_name);
                    if (response.response.success == true && response.response.status == 201)
                    {
                        App.Current.MainPage = new MainPage();
                    }

                    if (response.response.success == false)
                    {

                        if (response.response.status == 400)
                        {
                            await DisplayAlert("Fondo Merende", response.response.message, "OK");
                        }
                        else
                        {
                            await DisplayAlert("Fondo Merende", "Registrazione fallita", "OK");
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Fondo Merende", "Le password non combaciano", "OK");
                }
            }
            else
            {
                await DisplayAlert("Fondo Merende", "Inserire i campi obbligatori", "OK");
            }
        }

        private async void Cancel_ClickedAsync(object sender, EventArgs e) //Mostra il form di registrazione
        {
            await RegisterStack.FadeTo(0, 500);
            await RegisterStack.TranslateTo(0, 1000, 1);
            await LoginStack.TranslateTo(0, 0, 1);
            await LoginStack.FadeTo(1, 500);
        }
        private async void Donut_Background()
        {
            await Donut.RotateTo(10, 0);
            await Donut.ScaleTo(1.5, 0);
            while (true)
            { 
                await Donut.TranslateTo(20, -800, 100000);
                await Donut.TranslateTo(-20, 0, 1);
            }


        }
        private async void Ciambella()
        {
            await Fondo_Merende_logo.ScaleTo(0.2, 0);
            await Fondo_Merende_logo.TranslateTo(0, 0, 500);

            await Task.WhenAny<bool>
                (
                    Fondo_Merende_logo.ScaleTo(0.8, 500),
                    Fondo_Merende_logo.RotateTo(360, 500)
                );

            await Fondo_Merende_logo.ScaleTo(1.2, 150);
            await Fondo_Merende_logo.ScaleTo(1.0, 250);
            await Fondo_Merende_logo.TranslateTo(0, 0, 1500);

        }


        private async void LoginFade()
        {
            await RegisterStack.FadeTo(0, 1);
            await RegisterStack.TranslateTo(0, 1000, 1);
        }

    }

   
}

