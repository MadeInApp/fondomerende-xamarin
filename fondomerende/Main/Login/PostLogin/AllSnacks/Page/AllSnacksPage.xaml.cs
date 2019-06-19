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
using fondomerende.Main.Utilities;
using Rg.Plugins.Popup.Extensions;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup;

namespace fondomerende.Main.Login.PostLogin.AllSnack.Page
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllSnacksPage : ContentPage
    {
        SnackServiceManager snackServiceManager = new SnackServiceManager();
        List<SnackDataDTO> AllSnacks = new List<SnackDataDTO>();
        SnackDTO result;
        Dictionary<string, int> numerotocchi = new Dictionary<string, int>();
        bool switchStar = false;

        public AllSnacksPage()
        {
            numerotocchi.Add("numero", 0);
            InitializeComponent();
            GetSnacksMethod(false);
            Fade();
            animation();




            switch (Device.RuntimePlatform)                                                     //
            {                                                                                   //                                    
                                                                                                //   Se il dispositivo è Android non mostra la Top Bar della Navigation Page,
                case Device.Android:                                                            //      Se è iOS invece si (perchè senza è una schifezza)
                    NavigationPage.SetHasNavigationBar(this, false);                            //
                    break;                                                                      //
                                                                                                //
                default:                                                                        //
                    NavigationPage.SetHasNavigationBar(this, true);                             //
                    break;                                                                      //
            }                                                                                   //




            ListView.RefreshCommand = new Command(async () =>                                //
            {                                                                                //         
                await RefreshDataAsync();                                                    //
                ListView.IsRefreshing = false;                                               //
            });

            MessagingCenter.Subscribe<AllSnacksPage>(this, "Animation", async (value) =>
            {
                WalletAnimation();
            });
        }                                                                                    //
        public async Task RefreshDataAsync()                                                 //
        {                                                                                    //
            await GetSnacksMethod(true);                                                      //
        }                                                                                    //

        private void WalletAnimation()
        {
            Wallet.Play();
            Wallet.Speed = 3f;
        }


        public async Task GetSnacksMethod(bool Loaded)     //ottiene la lista degli snack e la applica alla ListView
        {
            result = await snackServiceManager.GetSnacksAsync();
            ListView.ItemsSource = result.data.snacks;
            if (!Loaded) //!WORKAROUND!   in questo modo si evita il crash ma la griglia non si aggiorna, urge investigazione sul vero problema
            {
                for (int i = 0; i <= result.data.snacks.Count; i++)
                {

                    ColorRandom c = new ColorRandom();
                    int box = 140;

                    var imageButton = new ImageButton
                    {
                        Margin = new Thickness(0, 20, 0, 20),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Scale = 2.6,
                        BackgroundColor = Color.White,
                        InputTransparent = true,
                        Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[i].friendly_name,
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
                        BorderWidth = 3,
                        InputTransparent = true,
                    };

                    var BordiSmussatiiOS = new RoundedCornerView
                    {
                        HeightRequest = box,
                        WidthRequest = box,
                        RoundedCornerRadius = box / 4,
                        BorderColor = c.GetRandomColor(),
                        BorderWidth = 1,
                        InputTransparent = true,
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
                        Orientation = StackOrientation.Vertical,
                    };
                    
                    var tgr = new TapGestureRecognizer();
                    tgr.Tapped += Tgr_Tapped;


                    app.GestureRecognizers.Add(tgr);

                    var tgr2 = new TapGestureRecognizer();


                    imageButton.Clicked += OnImageButtonClicked;
                    StackLayout.Children.Add(imageButton);

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:

                            BordiSmussatiAndroid.Children.Add(StackLayout);
                            app.Children.Add(BordiSmussatiAndroid);

                            break;

                        default:

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

        private void Tgr_Tapped(object sender, EventArgs e)
        {
               SnackDataDTO index = null;
            Wallet.IsPlaying = true;
            foreach (var item in (sender as StackLayout).Children)
            {
                if(item is Label)
                {
                    var snackName = (item as Label).Text;
                    index = result.data.snacks.Single(obj => obj.friendly_name == snackName);
                    break;
                }
                
            }
            if(index != null)
            {
                SelectedItemChangedEventArgs test = new SelectedItemChangedEventArgs(index);
                ListView_ItemSelected(null, test);
            }

        }

        private async void OnImageButtonClicked(object sender, EventArgs e)
        {
            var a = sender;
           // var ciao = sender.LoadFromXaml<>;
            //string section = Convert.ToString(Column0.Children);
            // section.RemoveAt(1);
        }

        private async void favourite_Clicked(object sender, EventArgs e)
        {
            switchStar = !switchStar;
            if(switchStar)
            {
                favourite.Source = ImageSource.FromResource("fondomerende.image.star_fill.png");
            }
            else
            {
                favourite.Source = ImageSource.FromResource("fondomerende.image.star_empty.png");
            }

        }


        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)     //quando uno snack è tappato si apre un prompt in cui viene chiesto se lo si vuole mangiare
        {
            var ans = await DisplayAlert("Fondo Merende", "Vuoi davvero mangiare " + (e.SelectedItem as SnackDataDTO).friendly_name + "?", "Si", "No");

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
                Swap.Play();
                Swap.Speed = -0.7f;

                ScrollView.IsVisible = false;
                ListView.IsVisible = true;
            }
            else
            {
                Swap.Play();
                Swap.Speed = 0.7f;

                ListView.IsVisible = false;
                ScrollView.IsVisible = true;
            }
        }

        private async void WalletClicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new DepositPopUp());
        }

        public void Timer()
        {

        }
        private async void animation()
        {
            EmbeddedImage e = new EmbeddedImage();
            e.Resource = "fondomerende.image.cup_cake_128x128.png";

            double ScreenHeight = Convert.ToInt32(App.Current.MainPage.Height);
            double ScreenWidth = Convert.ToInt32(App.Current.MainPage.Width);

            var paolo = new Image   //il cupcake paolo
            {
                Source = e.Resource,
                Opacity = 0.6,
                Scale = 1,
            };
            
            

            Random randomWidth = new Random((int)DateTime.Now.Ticks);
            double casual;
            double spawncasuale;


            await paolo.TranslateTo(0, -ScreenHeight / 2, 0);
            await paolo.TranslateTo(0, ScreenHeight, 10000);
            Cane.Children.Add(paolo); 
            //for (int f = 0; f < 20; f++)
            //{

            //    casual = randomWidth.Next(0, Convert.ToInt32(ScreenWidth));
            //    casual -= ScreenWidth / 2;
            //    spawncasuale = randomWidth.Next(0, Convert.ToInt32(ScreenWidth));
            //    spawncasuale -= ScreenWidth / 4;
            //    await paolo.TranslateTo(spawncasuale, -ScreenHeight/2, 0);
            //    await Task.WhenAny<bool>
            //    (
            //        paolo.RotateTo(360, 15000),
            //        paolo.TranslateTo(casual, ScreenHeight/2, 15000)
            //    );


            //}

        }

        public void Instagram()
        {
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired=2
            };
            tapGestureRecognizer.Tapped += SetFavourite;
        }


       private void SetFavourite(object sender,EventArgs e)
       {
            //if(Preferences.ContainsKey("Favourite")) => Preferences.Add("Favourite",); ;
            //Preferences.Add("",);
       }
        public async void Animazioni_infinite(Random randomWidth, double ScreenWidth,double ScreenHeight)
        {
            double casual;
            int i = 0;
            while (i<100)
            {
                
                i++;
            }
        }

    }

}
