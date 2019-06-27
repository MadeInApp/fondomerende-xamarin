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
using Lottie.Forms;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackListPage : AnimationPage
    {
        bool tgrBool;
        int idsnack = 0;
        public static int[] SelectedSnackIDArr;
        public static int SelectedSnackID;
        string snackName = null;
        ToBuySnackDTO result;
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
            MessagingCenter.Subscribe<BuySnackListPage>(this, "Close", async (arg) =>
            {
                Navigation.PopAsync();
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
                            InputTransparent = true,
                            BackgroundColor = Color.White,
                            Source = "http://fondomerende.madeinapp.net/api/getphoto.php?name=" + result.data.snacks[i].friendly_name.Replace(" ", "&nbsp;")
                        };

                        var imageButtoniOS = new ImageButton
                        {
                            InputTransparent = true,
                            Margin = new Thickness(0, 20, 0, 20),
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Scale = 2.6,
                            Source = "http://fondomerende.madeinapp.net/api/getphoto.php?name=" + result.data.snacks[i].friendly_name.Replace(" ", "&nbsp;")
                        };

                        var StackLayout = new StackLayout
                        {
                            WidthRequest = box,
                            HeightRequest = box,
                            BackgroundColor = Color.White,
                            InputTransparent = true,
                        };


                        var BordiSmussatiAndroid = new RoundedCornerView
                        {
                            HeightRequest = box,
                            WidthRequest = box,
                            RoundedCornerRadius = box / 2,
                            BorderColor = c.GetRandomColor(),
                            InputTransparent = true,
                            BorderWidth = 3,
                        };

                        var BordiSmussatiiOS = new RoundedCornerView
                        {
                            HeightRequest = box,
                            WidthRequest = box,
                            RoundedCornerRadius = box / 4,
                            InputTransparent = true,
                            BorderColor = c.GetRandomColor(),
                            BorderWidth = 1,
                        };

                        var label = new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.End,
                            Text = result.data.snacks[i].friendly_name,
                            FontSize = 12,
                            InputTransparent = true,
                        };


                        var app = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical
                        };

                        var tgr = new TapGestureRecognizer();
                        tgr.Tapped += Tgr_Tapped;
                        app.GestureRecognizers.Add(tgr);

                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                StackLayout.Children.Add(imageButtonAndroid);
                                BordiSmussatiAndroid.Children.Add(StackLayout);
                                app.Children.Add(BordiSmussatiAndroid);

                                break;

                            default:
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


        private async void Tgr_Tapped(object sender, EventArgs e)
        {
            var result = await SnackService.GetToBuySnacksAsync();
            ToBuyDataDTO index = null;
            foreach (var item in (sender as StackLayout).Children)
            {
                if (item is Label)
                {
                    snackName = (item as Label).Text;
                    foreach(var i in result.data.snacks)
                    {
                        if (i.friendly_name == snackName)
                        {
                            idsnack = i.id;
                            index = i;
                            break;
                        }    
                    } 
                }
            }
            if (index != null)
            {
                tgrBool = true;
                SelectedItemChangedEventArgs test = new SelectedItemChangedEventArgs(index);
                ListView_ItemSelected(null, test);
            }

        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(tgrBool)
            {
                tgrBool = false;
                SelectedSnackID = idsnack;
            }
            else
            {
                await SnackService.GetToBuySnacksAsync();
                SelectedSnackID = (e.SelectedItem as ToBuyDataDTO).id;
            }
            await Navigation.PushPopupAsync(new BuySnackPopUpPage());

        }

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

