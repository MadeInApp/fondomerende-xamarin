using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using fondomerende.Services.Models;
using Xamarin.Forms.Xaml;
using fondomerende.PostLoginPages;

namespace fondomerende.GraphicInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSnackListPage : ContentPage
    {
        public static string SelectedSnack;
        SnackServiceManager SnackService = new SnackServiceManager();
        public EditSnackListPage()
        {
            InitializeComponent();
            GetSnacksMethod();

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




            ListView.RefreshCommand = new Command(async () =>                                //
            {                                                                                //         
                await RefreshDataAsync();                                                    //
                ListView.IsRefreshing = false;                                               //
            });                                                                              //
                                                                                             //
                                                                                             // Pull to Refresh GetSnacksMethod()
        }                                                                                    //
        public async Task RefreshDataAsync()                                                 //
        {                                                                                    //
            await GetSnacksMethod();                                                         //
        }                                                                                    //



        public async Task GetSnacksMethod()     //ottiene la lista degli snack e la applica alla ListView
        {
            LoginServiceManager login = new LoginServiceManager();
            var resultLogin = await login.LoginAsync(Preferences.Get("username", ""), Preferences.Get("password", ""), true);
            if (!resultLogin.response.success)
            {
                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            var result = await SnackService.GetSnacksAsync();
            ListView.ItemsSource = result.data.snacks;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditSnackSettingsPage());
            SelectedSnack = (e.SelectedItem as SnackDataDTO).name;
        }

    }
}



