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
using fondomerende.PostLoginPages.GraphicInterfaces;
using Rg.Plugins.Popup.Extensions;

namespace fondomerende.PostLoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackListPage : ContentPage
    {
        public static int SelectedSnackID;
        public static bool Refresh = false;
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
            var result = await SnackService.GetToBuySnacksAsync();
            ListView.ItemsSource = result.data.snacks;
            int z;
            for (int i = 0; i <= result.data.snacks.Count; i++)
            {
                if (i % 2 == 0)
                {
                    z = i;
                    Column0.Children.Add(new ImageButton { Scale = 2, Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[z].friendly_name.Replace(" ", "") });
                    Column0.Children.Add(new Label { HorizontalTextAlignment = TextAlignment.Center, Text = result.data.snacks[z].friendly_name });
                }
                else
                {
                  Column1.Children.Add(new ImageButton {Scale = 2, Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[i].friendly_name.Replace(" ", "") });
                  Column1.Children.Add(new Label { HorizontalTextAlignment = TextAlignment.Center, Text = result.data.snacks[i].friendly_name });
                }
            }
        }

 

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var result = await SnackService.GetToBuySnacksAsync();
            SelectedSnackID = (e.SelectedItem as ToBuyDataDTO).id;
            await Navigation.PushPopupAsync(new BuySnackPopUpPage());

        }
    }
}

