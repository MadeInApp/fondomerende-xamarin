using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Services.Models;
using fondomerende.Main.Manager;
using fondomerende.PostLoginPages.GraphicInterfaces;

namespace fondomerende.PostLoginPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllSnacksPage : ContentPage
    {
        SnackServiceManager snackServiceManager = new SnackServiceManager();
        List<SnackDataDTO> AllSnacks = new List<SnackDataDTO>();


        public AllSnacksPage()
        {
                      
            InitializeComponent();
            GetSnacksMethod();
            Fade();


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
           await GetSnacksMethod();                                                          //
        }                                                                                    //



        public async Task GetSnacksMethod()     //ottiene la lista degli snack e la applica alla ListView
        {
          
            var result = await snackServiceManager.GetSnacksAsync();
            ListView.ItemsSource = result.data.snacks;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)     //quando uno snack è tappato si apre un prompt in cui viene chiesto se lo si vuole mangiare
        {
            var ans =  await DisplayAlert("Fondo Merende", "Vuoi davvero mangiare " + (e.SelectedItem as SnackDataDTO).friendly_name + "?", "Si", "No");

            if (ans == true)
            {
              await snackServiceManager.EatAsync((e.SelectedItem as SnackDataDTO).id, 1);
              await GetSnacksMethod();
            } 
            else
            {
               await GetSnacksMethod();
            }
        }

        private async void Fade()
        {
            await StackSnack.FadeTo(0, 0);
            await StackSnack.FadeTo(1, 1400);
        }
    }
}
