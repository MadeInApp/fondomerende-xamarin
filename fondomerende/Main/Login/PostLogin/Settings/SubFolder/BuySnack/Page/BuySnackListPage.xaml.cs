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
using fondomerende.Main.Utilities;
using FormsControls.Base;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackListPage : AnimationPage
    {
        SnackServiceManager snackServiceManager = new SnackServiceManager();
        public static int[] SelectedSnackIDArr;
        public static int SelectedSnackID;
        public static bool Refresh = false;
        SnackServiceManager SnackService = new SnackServiceManager();
        public ObservableCollection<string> Items { get; set; }

        public BuySnackListPage()
        {
            InitializeComponent();
            GetSnacksMethod(false);
            MessagingCenter.Subscribe<BuySnackListPage>(this, "Refresh", async (value) =>
            {
                await GetSnacksMethod(true);
            });



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

        public async Task GetSnacksMethod(bool Loaded)     //ottiene la lista degli snack e la applica alla ListView
        {
           var result = await SnackService.GetToBuySnacksAsync();
           ListView.ItemsSource = result.data.snacks;
            if (result != null)
            {
                //ListView.ItemsSource = result.data.snacks;
                if (!Loaded) //!WORKAROUND!   in questo modo si evita il crash ma la griglia non si aggiorna, urge investigazione sul vero problema
                {
                    for (int i = 0; i <= result.data.snacks.Count; i++)
                    {

                        ColorRandom c = new ColorRandom();
                        int box = 140;

                        var imageButtonAndroid = new ImageButton
                        {
                            Margin = new Thickness(0, 20, 0, 20),
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Scale = 3,
                            BackgroundColor = Color.White,
                            Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[i].friendly_name.Replace(" ", "&nbsp;")
                        };

                        var imageButtoniOS = new ImageButton
                        {
                            Margin = new Thickness(0, 20, 0, 20),
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Scale = 2.6,
                            Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[i].friendly_name.Replace(" ", "&nbsp;")
                        };

                        var StackLayout = new StackLayout
                        {
                            WidthRequest = box,
                            HeightRequest = box,
                            BackgroundColor = Color.White,
                        };


                        var BordiSmussatiAndroid = new RoundedCornerView
                        {
                            HeightRequest = box,
                            WidthRequest = box,
                            RoundedCornerRadius = box / 2,
                            BorderColor = c.GetRandomColor(),
                            BorderWidth = 3,
                        };

                        var BordiSmussatiiOS = new RoundedCornerView
                        {
                            HeightRequest = box,
                            WidthRequest = box,
                            RoundedCornerRadius = box / 4,
                            BorderColor = c.GetRandomColor(),
                            BorderWidth = 1,
                        };

                        var label = new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.End,
                            Text = result.data.snacks[i].friendly_name,
                            FontSize = 12,
                        };


                        var app = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical
                        };

                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                imageButtonAndroid.Clicked += OnImageButtonClicked;
                                StackLayout.Children.Add(imageButtonAndroid);
                                BordiSmussatiAndroid.Children.Add(StackLayout);
                                app.Children.Add(BordiSmussatiAndroid);

                                break;

                            default:
                                imageButtoniOS.Clicked += OnImageButtonClicked;
                                StackLayout.Children.Add(imageButtoniOS);
                                BordiSmussatiiOS.Children.Add(StackLayout);
                                app.Children.Add(BordiSmussatiiOS);
                                break;
                        }

                        app.Children.Add(label);
                        if (i % 2 == 0) Column0.Children.Add(app);
                        else Column1.Children.Add(app);
                    }
                }
            }
            else
            {

            }
        }


        async void OnImageButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new BuySnackPopUpPage());
        }


        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)     //quando uno snack è tappato si apre un prompt in cui viene chiesto se lo si vuole mangiare
        {
            var ans = await DisplayAlert("Fondo Merende", "Vuoi davvero mangiare " + (e.SelectedItem as SnackDataDTO).friendly_name + "?", "Si", "No");

            if (ans == true)
            {

                await snackServiceManager.EatAsync((e.SelectedItem as SnackDataDTO).id, 1);
                MessagingCenter.Send(new BuySnackPopUpPage()
                {

                }, "RefreshUF");
                await GetSnacksMethod(true);
            }
            else
            {
                await GetSnacksMethod(true);
            }
        }


        /*private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await SnackService.GetToBuySnacksAsync();
            SelectedSnackID = (e.SelectedItem as ToBuyDataDTO).id;
            await Navigation.PushPopupAsync(new BuySnackPopUpPage());

        }*/

        private void Swap_Clicked(object sender, EventArgs e)
        {
            if(ScrollView.IsVisible == true)
            {
                Swap.BackgroundColor = Color.OrangeRed;
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
    }

}

