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

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSnackListPage : ContentPage
    {
        public static string SelectedSnackName;
        public static string SelectedSnackFriendlyName;
        public static double SelectedSnackPrice;
        public static int SelectedSnackPerBox;
        public static int SelectedSnackExpiration;
        public static int SelectedSnackQuantity;
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
            var result = await SnackService.GetAllSnacksAsync();
            ListView.ItemsSource = result.data.snacks;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedSnackFriendlyName = (e.SelectedItem as SnackDataDTO).friendly_name;
            SelectedSnackName = (e.SelectedItem as SnackDataDTO).name;
            await Navigation.PushAsync(new EditSnackSettingsPage());
        }

    }
}



