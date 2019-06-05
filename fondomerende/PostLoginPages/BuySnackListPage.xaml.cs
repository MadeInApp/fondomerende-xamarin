using fondomerende.Services.RESTServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.PostLoginPages;
using fondomerende.Services.Models;

namespace fondomerende.PostLoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackListPage : ContentPage
    {
        SnackServiceManager SnackService = new SnackServiceManager();
        public ObservableCollection<string> Items { get; set; }

        public BuySnackListPage()
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



        }

        public async Task GetSnacksMethod()     //ottiene la lista degli snack e la applica alla ListView
        {
            var resultTrue = await SnackService.GetAllSnacksAsync();



            ListView.ItemsSource = resultTrue.data.snacks;

        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {


        }

    }
}

