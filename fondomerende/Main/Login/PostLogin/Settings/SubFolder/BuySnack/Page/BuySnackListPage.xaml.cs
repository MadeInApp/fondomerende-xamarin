using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Services.Models;
using Rg.Plugins.Popup.Extensions;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Popup;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page
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
                    var imageButton_z = new ImageButton
                    {

                    Scale = 2,
                        Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[z].friendly_name.Replace(" ", "")

                    };

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            imageButton_z.BackgroundColor = Color.Transparent;
                            break;
                    }

                    var label_z = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = result.data.snacks[z].friendly_name
                    };

                    imageButton_z.Clicked += OnImageButtonClicked;
                    Column0.Children.Add(imageButton_z);
                    Column0.Children.Add(label_z);
                }
                else
                {
                  Column1.Children.Add(new ImageButton {Scale = 2, Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[i].friendly_name.Replace(" ", "") });
                  Column1.Children.Add(new Label { HorizontalTextAlignment = TextAlignment.Center, Text = result.data.snacks[i].friendly_name });
                }
            }
        }


        private async void OnImageButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new BuySnackPopUpPage());
        }


        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var result = await SnackService.GetToBuySnacksAsync();
            SelectedSnackID = (e.SelectedItem as ToBuyDataDTO).id;
            await Navigation.PushPopupAsync(new BuySnackPopUpPage());

        }
    }

}

