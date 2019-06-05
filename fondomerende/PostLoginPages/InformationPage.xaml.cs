using fondomerende.Services.RESTServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using fondomerende.PostLoginPages.GraphicInterfaces;
using fondomerende.PostLoginPages.GraphicInterfaces.SubInterfaces;

namespace fondomerende.PostLoginPages
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
                    var section = tableView.Root[0];
                    section.RemoveAt(1);
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
                if (response.response.success == true)
                {
                    App.Current.MainPage = new LoginPage();
                    Preferences.Clear();
                }
                else
                {
                    await DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
                }
            }         
        }

      

        private void ChangeUserSettings_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditUserPage());
        }

        private void EditSnackViewCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditSnackListPage());
        }

        private void AddSnackCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddSnackPage());
        }

        private void ChronologyCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChronologyLog());
        }

        private void BuySnack_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BuySnackListPage());
        }
    }
}