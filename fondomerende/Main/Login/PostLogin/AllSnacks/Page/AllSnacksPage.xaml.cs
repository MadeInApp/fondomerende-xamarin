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
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup;

namespace fondomerende.Main.Login.PostLogin.AllSnack.Page
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllSnacksPage : ContentPage
    {
        SnackServiceManager snackServiceManager = new SnackServiceManager();
        List<SnackDataDTO> AllSnacks = new List<SnackDataDTO>();


        public AllSnacksPage()
        {

            InitializeComponent();
            GetSnacksMethod(false);
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
           await GetSnacksMethod(true);                                                          //
        }                                                                                    //



        public async Task GetSnacksMethod(bool Loaded)     //ottiene la lista degli snack e la applica alla ListView
        {
            var result = await snackServiceManager.GetSnacksAsync();
            ListView.ItemsSource = result.data.snacks;
            int z = 0;
            if (!Loaded) //!WORKAROUND!   in questo modo si evita il crash ma la griglia non si aggiorna, urge investigazione sul vero problema
            {
                for (int i = 0; i <= result.data.snacks.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        z = i;
                        var StackLayout_z = new StackLayout { Spacing = 10, HeightRequest = 100 };
                        var imageButton_z = new ImageButton
                        {
                            Margin = new Thickness(0, 20, 0, 20),
                            Scale = 2,
                            Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[z].friendly_name.Replace(" ", "&nbsp;") + "_500x500"
                        };

                        var label_z = new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = result.data.snacks[z].friendly_name
                        };


                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                imageButton_z.BackgroundColor = Color.Transparent;
                                break;
                        }
                        imageButton_z.Clicked += OnImageButtonClicked;
                        StackLayout_z.Children.Add(imageButton_z);
                        StackLayout_z.Children.Add(label_z);
                        Column0.Children.Add(StackLayout_z);

                    }
                    else
                    {
                        var StackLayout_i = new StackLayout { Spacing = 10, HeightRequest = 100 };
                        var imageButton_i = new ImageButton
                        {
                            ScaleY = 1,
                            ScaleX = 1,
                            Margin = new Thickness(0, 20, 0, 20),
                            Scale = 2,
                            Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[i].friendly_name.Replace(" ", "")
                        };

                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                imageButton_i.BackgroundColor = Color.Transparent;
                                break;
                        }

                        var label_i = new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = result.data.snacks[i].friendly_name
                        };

                        imageButton_i.Clicked += OnImageButtonClicked;
                        StackLayout_i.Children.Add(imageButton_i);
                        StackLayout_i.Children.Add(label_i);
                        Column1.Children.Add(StackLayout_i);
                    }
                }
            }
        }

        async void OnImageButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Fondo merende", "test", "test");
        }


        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)     //quando uno snack è tappato si apre un prompt in cui viene chiesto se lo si vuole mangiare
        {
            var ans =  await DisplayAlert("Fondo Merende", "Vuoi davvero mangiare " + (e.SelectedItem as SnackDataDTO).friendly_name + "?", "Si", "No");

            if (ans == true)
            {
              await snackServiceManager.EatAsync((e.SelectedItem as SnackDataDTO).id, 1);
              await GetSnacksMethod(true);
            } 
            else
            {
               await GetSnacksMethod(true);
            }
        }

        private async void Fade()
        {
            await StackSnack.FadeTo(0, 0);
            await StackSnack.FadeTo(1, 1400);
        }
        private void Swap_Clicked(object sender, EventArgs e)
        {
            if (ScrollView.IsVisible == true)
            {
                Swap.BackgroundColor = Color.Orange;
                ScrollView.IsVisible = false;
                ListView.IsVisible = true;
            }
            else
            {
                Swap.BackgroundColor = Color.Transparent;
                ListView.IsVisible = false;
                ScrollView.IsVisible = true;
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new DepositPopUp());
        }
    }

}
