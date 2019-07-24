using fondomerende.Main.Services.RESTServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using fondomerende.Main.Login.LoginPages;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.Content;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Popup;
using Rg.Plugins.Popup.Extensions;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.About_and_UserSettings.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.LogOut.View;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;

namespace fondomerende.Main.Login.PostLogin.Settings.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationPage : ContentPage
    {
        UserServiceManager UserService = new UserServiceManager();
        
        public object LoggedAs { get; }
        public string friendly_name = Preferences.Get("username", "");

        public InformationPage()
        {
            InitializeComponent();
            EditUserViewCell.BindingContext = friendly_name;
        
            switch (Device.RuntimePlatform)             //Se il dispositivo è Android non mostra la Top Bar della Navigation Page, se è iOS la mostra
            {
                default:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                   // var section = tableView.Root[0];
                   // section.RemoveAt(1);
                    break;

            }
            

        }

        private bool DevicePlatform(bool v1, bool v2, bool v3)
        {
            throw new NotImplementedException();
        }

        private async void LogOut_button_Clicked(object sender, EventArgs e)        //effettua il Log Out
        {
            var ans = await DisplayAlert("Fondo Merende", "Sicuro di voler effettuare il Log Out?", "Si", "No");
            if(ans)
            {
                LogoutServiceManager logoutService = new LogoutServiceManager();
                var response = await logoutService.LogoutAsync();
                if (response != null)
                {
                    if (response.success == true)
                    {
                        App.Current.MainPage = new LoginPage();
                        Preferences.Clear();
                    }
                    else
                    {
                        await DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
                    }
                }
                else
                {

                }
            }         
        }

      

        private async void ChangeUserSettings_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        private async void EditSnackViewCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditSnackListPage());

        }

        private async void AddSnackCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new AddSnackPopUpPage());
        }

        private async void ChronologyCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChronologyContentPage());
        }

        private async void BuySnack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuySnackListPage());
        }

        private async void Deposit_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new DepositPopUp());
        }
    }
}