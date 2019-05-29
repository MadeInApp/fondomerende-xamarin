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
    public partial class RegisterPage : ContentPage
    {
        private string username, friendly_name, password, testpassword;

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        public RegisterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void RegisterButton_ClickedAsync(object sender, EventArgs e)
        {
            //SnackServiceManager snackService = new SnackServiceManager();
            //var a = await snackService.GetSnacksAsync();
            if (!string.IsNullOrEmpty(usernameEntry.Text) && !string.IsNullOrEmpty(friendlyNameEntry.Text) && !string.IsNullOrEmpty(passwordEntry.Text) && !string.IsNullOrEmpty(testPasswordEntry.Text))
            {
                password = passwordEntry.Text;
                testpassword = testPasswordEntry.Text;
                username = usernameEntry.Text;
                friendly_name = friendlyNameEntry.Text;

                if (password == testpassword)
                {

                    RegisterServiceManager registerService = new RegisterServiceManager();
                    var response = await registerService.RegisterAsync(username, friendly_name, password);
                    if (response.response.success == true && response.response.status == 201)
                    {
                        App.Current.MainPage = new MainPage();
                    }

                    if (response.response.success == false)
                    {
                    
                        if(response.response.status == 400)
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

    }
}

