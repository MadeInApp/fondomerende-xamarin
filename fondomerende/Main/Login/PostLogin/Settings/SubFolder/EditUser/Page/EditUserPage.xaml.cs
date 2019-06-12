using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Services.RESTServices;
using fondomerende.Main.Login.LoginPages;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Popup;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Page
{
    public partial class EditUserPage : ContentPage
    {
        public static string username = "";
        public static string FriendlyName = "";
        public static string passwordNuova = "";

        public string GetpasswordNuova()
        {
            return passwordNuova;
        }

        private void SetpasswordNuova(string value)
        {
            passwordNuova = value;
        }

        public static bool clicked = false;
        public EditUserPage()
        {
            InitializeComponent();
            usernameEntry.Placeholder = Preferences.Get("username", null) ;
            friendlynameEntry.Placeholder = Preferences.Get("friendly-name", null);
            switch (Device.RuntimePlatform)                                                     //
            {                                                                                   //                                    
                                                                                                //   Se il dispositivo è Android non mostra la Top Bar della Navigation Page,
                case Device.Android:                                                            //      Se è iOS invece si (perchè senza è una schifezza)
                    NavigationPage.SetHasNavigationBar(this, false);                            //
                    break;                                                                      //
                                                                                                //
                default:                                                                    //
                    NavigationPage.SetHasNavigationBar(this, true);                             //
                    break;                                                                      //
            }                                                                                   //

        }

        private async void ApplyChanges_Clicked_1(object sender, EventArgs e)
        {
            EditUserServiceManager editUser = new EditUserServiceManager();
            if (usernameEntry.Text != null)
            {
                username = usernameEntry.Text;
            }
            else
            {
                await DisplayAlert("Fondo Merende", "username mancante", "Ok");
            }
            if (friendlynameEntry.Text != null)
            {
                FriendlyName = friendlynameEntry.Text;
            }
            else
            {
                await DisplayAlert("Fondo Merende", "nome mancante", "Ok");
            }
            if (passwordEntry.Text != null)
            {
                SetpasswordNuova(passwordEntry.Text);
            }
            else
            {
                await DisplayAlert("Fondo Merende", "password mancante", "Ok");
            }
            if (usernameEntry.Text != null && friendlynameEntry.Text != null && passwordEntry.Text != null)
            {
                Navigation.PushPopupAsync(new EditUserPopUpPage());
            }

            /*
            else
            {
                var risp = await editUser.EditUserAsync(username, FriendlyName, passwordNuova);
                if (risp.response.success == true)
                {

                    await DisplayAlert("Fondo Merende", "Impostazioni Cambiate", "Ok");
                    Xamarin.Essentials.Preferences.Clear();
                    Application.Current.MainPage = new LoginPage();

                }
                else
                {
                    await DisplayAlert("Fondo Merende", "Password errata", "Ok");
                }

            }  
        }
        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();

        }*/
        }
    }
}