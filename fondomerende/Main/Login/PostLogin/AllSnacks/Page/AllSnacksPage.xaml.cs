using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fondomerende.Main.Login.PostLogin.AllSnacks.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using fondomerende.Main.Services.Models;
using fondomerende.Main.Services.RESTServices;
using fondomerende.Main.Utilities;
using Lottie.Forms;
using MR.Gestures;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.AllSnack.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllSnacksPage
    {
        public static double priceBinding;
        int eatLoading = 0;

        public static string selectedItemBinding { get; set; }
        SnackServiceManager snackServiceManager = new SnackServiceManager();
        List<SnackDataDTO> AllSnacks = new List<SnackDataDTO>();
        SnackDTO result;
        Dictionary<string, int> numerotocchi = new Dictionary<string, int>();
        bool switchStar = false;
        AnimationView Swap;

        string previousFavourite;

        object[] Snackarray = new object[100];
        object[] SnackFavarray;

        public AllSnacksPage()
        {
            numerotocchi.Add("numero", 0);
            InitializeComponent();
            GetSnacksMethod(false, false);
            GetSnacksMethod(false, true);

            previousFavourite = Preferences.Get("Favourites", "");

            Fade();
            animation();
            MessagingCenter.Subscribe<AllSnacksPage>(this, "RefreshGetSnacks", async (arg) =>
            {
                GetSnacksMethod(false, false);
            });




            switch (Device.RuntimePlatform)                                                    //\\
            {                                                                                  // \\                                    
                                                                                               //  \\ Se il dispositivo è Android non mostra la Top Bar della Navigation Page,
                case Device.Android:                                                           //   \\   Se è iOS invece si (perchè senza è una schifezza)
                    NavigationPage.SetHasNavigationBar(this, false);                   ///     //    \\         \                
                    break;                                                               ////// ////// ///////////|
                                                                                         ///     //     //        /       
                default:                                                                       //    //
                    NavigationPage.SetHasNavigationBar(this, true);                            //   //
                    break;                                                                     // //
            }                                                                                  ////


            Swap = new AnimationView
            {
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Animation = "list2grid_alt.json",
                Margin = new Thickness(0, 0, 0, 10),
                AutoPlay = false,
            };
            //Swap.OnClick += Swap_Clicked;
            //GridView1.Children.Add(Swap, 0, 0);


            ListView.RefreshCommand = new Command(async () =>
            {
                await RefreshDataAsync();
                ListView.IsRefreshing = false;
            });

            MessagingCenter.Subscribe<AllSnacksPage>(this, "Animation", async (value) =>
            {
                WalletAnimation();
            });

        }
        public async Task RefreshDataAsync()
        {
            await GetSnacksMethod(true, false);
        }

        public async Task RefreshFavouriteDataAsync()
        {
            await GetSnacksMethod(true, true);
        }

        private void WalletAnimation()
        {
            Wallet.Play();
            Wallet.Speed = 1f;
        }




        public async Task GetSnacksMethod(bool Loaded, bool favourites)     //ottiene la lista degli snack e la applica alla ListView
        {
            result = await snackServiceManager.GetSnacksAsync();
            SnackFavarray = new object[result.data.snacks.Count];
            ListView.ItemsSource = result.data.snacks;
            int FavIndex = 0;
            int index = 0;

            if (!Loaded) //!WORKAROUND!   in questo modo si evita il crash ma la griglia non si aggiorna, urge investigazione sul vero problema
            {
                for (int i = 0; i <= result.data.snacks.Count; i++)
                {
                    bool addfav = false; //variabile di appoggio
                    bool visibilità = true;
                    if (favourites && !Check_Favourites(result.data.snacks[i].id))
                    {
                        addfav = true;
                    }
                    else if (favourites && Check_Favourites(result.data.snacks[i].id))
                    {
                        visibilità = false;
                    }

                    ColorRandom c = new ColorRandom();
                    var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

                    double box = 140;

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


                    var StackLayout = new MR.Gestures.StackLayout
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

                    var label = new MR.Gestures.Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.End,
                        Text = result.data.snacks[i].friendly_name,
                        FontSize = 12,
                        InputTransparent = true,
                    };

                    string e;
                    if (Check_Favourites(result.data.snacks[i].id))
                    {
                        e = "fondomerende.image.star_empty.png";
                    }
                    else
                    {
                        e = "fondomerende.image.star_fill.png";
                    }

                    var star = new MR.Gestures.Image
                    {
                        HeightRequest = 20,
                        WidthRequest = 20,
                        Margin = new Thickness(0, 15, 15, 0),
                        Scale = 1,
                        Aspect = Aspect.Fill,
                        Source = ImageSource.FromResource(e),
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                    };

                    var starAnimation = new AnimationView
                    {
                        Animation = "star.json",
                        Scale = 1,
                        Loop = false,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        AutoPlay = false,
                        InputTransparent = true,
                        IsVisible = false,
                    };


                    var eatAnimation = new AnimationView
                    {
                        Animation = "LoadingEating.json",
                        Scale = 1.2,
                        Rotation = 180,
                        Loop = false,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        AutoPlay = false,
                        InputTransparent = true,
                        IsVisible = false,
                    };


                    var app = new MR.Gestures.StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        IsVisible = visibilità,
                    };

                    starAnimation.OnFinish += StopAnimation;

                    StackLayout.Children.Add(imageButton);

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:

                            BordiSmussatiAndroid.Children.Add(StackLayout);

                            BordiSmussatiAndroid.Children.Add(starAnimation);
                            BordiSmussatiAndroid.Children.Add(star);
                            BordiSmussatiAndroid.Children.Add(eatAnimation);


                            app.Children.Add(BordiSmussatiAndroid);

                            break;

                        default:

                            BordiSmussatiiOS.Children.Add(StackLayout);

                            BordiSmussatiiOS.Children.Add(starAnimation);
                            BordiSmussatiiOS.Children.Add(star);
                            BordiSmussatiiOS.Children.Add(eatAnimation);


                            app.Children.Add(BordiSmussatiiOS);
                            break;
                    }

                    app.Children.Add(label);


                    app.LongPressed += Stack_LongPressed;
                    app.LongPressing += Stack_LongPressing;


                    app.DoubleTapped += Tgr2_Tapped;


                    if (addfav && favourites)
                    {
                        if (FavIndex % 2 == 0) Column0Fav.Children.Add(app);
                        else Column1Fav.Children.Add(app);


                        Snackarray[FavIndex] = app;
                        FavIndex++;
                    }
                    else
                    {
                        if (i % 2 == 0) Column0.Children.Add(app);
                        else Column1.Children.Add(app);

                        SnackFavarray[index] = app;
                        index++;
                    }

                }
            }
        }




        public async Task refreshFavAsync()
        {
            if (previousFavourite != Preferences.Get("Favourites", ""))
            {
                previousFavourite = Preferences.Get("Favourites", "");
                Column0Fav.Children.Clear();
                Column1Fav.Children.Clear();

                GetSnacksMethod(false, true);
            }

        }

        public async Task refreshSnackAsync()
        {
            if (previousFavourite != Preferences.Get("Favourites", ""))
            {
                previousFavourite = Preferences.Get("Favourites", "");
                Column0.Children.Clear();
                Column1.Children.Clear();
                GetSnacksMethod(false, false);
            }

        }

        private void StopAnimation(object sender, EventArgs e)
        {
            (sender as AnimationView).FadeTo(0, 300);
        }

        private bool Check_Favourites(int id)
        {
            string preferito = "";

            string fav = Convert.ToString(id);
            string getfav = Preferences.Get("Favourites", "");

            string[] strSplit = getfav.Split(',');


            for (int i = 0; i < strSplit.Length; i++)
            {

                if (Convert.ToString(id) == strSplit[i])
                {
                    preferito = strSplit[i];
                    break;
                }
            }
            if (preferito != "") //se è gia presente
            {
                return false;
            }
            else //se non è presente
            {

                return true;
            }
        }

        private bool Check_FavouritesAndSet(int id)
        {
            string preferito = "";

            string fav = Convert.ToString(id);
            string getfav = Preferences.Get("Favourites", "");

            string[] strSplit = getfav.Split(',');


            for (int i = 0; i < strSplit.Length; i++)
            {

                if (Convert.ToString(id) == strSplit[i])
                {
                    preferito = strSplit[i];
                    break;
                }
            }
            if (preferito != "") //se è gia presente
            {
                string newpreferiti = "";

                for (int i = 0; i < strSplit.Length; i++)
                {
                    if (strSplit[i] != fav)
                    {
                        if (getfav == "") newpreferiti += strSplit[i];
                        else newpreferiti += strSplit[i] + ",";
                    }
                }
                Preferences.Set("Favourites", newpreferiti);
                return false;
            }
            else //se non lo è
            {
                string concatena = fav + "," + getfav;
                Preferences.Set("Favourites", concatena);
                return true;
            }
        }

        private async void Tgr2_Tapped(object sender, EventArgs e)
        {
            SnackDataDTO index = null;
            foreach (var item in (sender as MR.Gestures.StackLayout).Children)
            {
                if (item is MR.Gestures.Label)
                {
                    var snackName = (item as MR.Gestures.Label).Text;
                    index = result.data.snacks.Single(obj => obj.friendly_name == snackName);
                    break;
                }

            }
            if (index != null)
            {
                string preferito = "";

                foreach (var app in (sender as MR.Gestures.StackLayout).Children)
                {
                    if (app is RoundedCornerView)
                    {
                        foreach (var an in (app as RoundedCornerView).Children)
                        {
                            if (an is AnimationView)
                            {
                                AnimationView ap = (AnimationView)an;
                                if (ap.Animation == "star.json")
                                {
                                    if (Check_FavouritesAndSet(index.id))
                                    {
                                        ap.IsVisible = true;
                                        ap.FadeTo(1);
                                        ap.Play();
                                    }
                                }

                            }

                            if (an is MR.Gestures.Image)
                            {
                                MR.Gestures.Image image = (MR.Gestures.Image)an;
                                string a = "";
                                if (Check_Favourites(index.id))
                                {
                                    a = "fondomerende.image.star_empty.png";
                                }
                                else
                                {
                                    a = "fondomerende.image.star_fill.png";
                                }


                                image.Source = ImageSource.FromResource(a);
                            }
                        }
                    }
                }
            }
        }

        private void Tgr_Tapped(object sender, EventArgs e)
        {
            SnackDataDTO index = null;
            foreach (var item in (sender as MR.Gestures.StackLayout).Children)
            {
                if (item is MR.Gestures.Label)
                {
                    var snackName = (item as MR.Gestures.Label).Text;
                    index = result.data.snacks.Single(obj => obj.friendly_name == snackName);
                    break;
                }

            }
            if (index != null)
            {
                SelectedItemChangedEventArgs test = new SelectedItemChangedEventArgs(index);
                ListView_ItemSelected(null, test);
            }

        }

        private async void favourite_Clicked(object sender, EventArgs e)
        {
            switchStar = !switchStar;
            if (switchStar)
            {
                await refreshFavAsync();
                ScrollSnackView.IsVisible = false;
                ScrollFavourites.IsVisible = true;
                ListView.IsVisible = false;
                favourite.Source = ImageSource.FromResource("fondomerende.image.star_fill.png");
            }
            else
            {
                await refreshSnackAsync();
                ScrollSnackView.IsVisible = true;
                ListView.IsVisible = false;
                ScrollFavourites.IsVisible = false;
                favourite.Source = ImageSource.FromResource("fondomerende.image.star_empty.png");
            }

        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)     //quando uno snack è tappato si apre un prompt in cui viene chiesto se lo si vuole mangiare
        {
            var ans = await DisplayAlert("Fondo Merende", "Vuoi davvero mangiare " + (e.SelectedItem as SnackDataDTO).friendly_name + "?", "Si", "No");
            if (ans == true)
            {
                EatDTO response = await snackServiceManager.EatAsync((e.SelectedItem as SnackDataDTO).id, 1);
            }
            selectedItemBinding = (e.SelectedItem as SnackDataDTO).friendly_name;

            MessagingCenter.Send(new EditUserViewCell()
            {

            }, "RefreshUF");
            

            MessagingCenter.Send(new SnackViewCell()
            {

            }, "Animate");

            ListView.SelectionMode = ListViewSelectionMode.Single;
        }

        private async void Fade()
        {
            await StackSnack.FadeTo(0, 0);
            await StackSnack.FadeTo(1, 1400);
        }

        private void Swap_Clicked(object sender, EventArgs e)
        {
            if (ScrollSnackView.IsVisible == true)
            {
                ListToGrid.BackgroundColor = Color.OrangeRed;
                ScrollSnackView.IsVisible = false;
                ListView.IsVisible = true;

                ScrollFavourites.IsVisible = false;
                ScrollSnackView.IsVisible = false;
                ListView.IsVisible = true;
                favourite.Source = ImageSource.FromResource("fondomerende.image.star_empty.png");
            }
            else
            {
                ListToGrid.BackgroundColor = Color.Transparent;
                ListView.IsVisible = false;
                ScrollFavourites.IsVisible = false;
                ScrollSnackView.IsVisible = true;
            }
        }

        private async void WalletClicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new DepositPopUp());
        }

        private void animation()
        {

            var mainDisplayWidth = DeviceDisplay.MainDisplayInfo.Width;
            var mainDisplayHeight = DeviceDisplay.MainDisplayInfo.Height;
            int numeroMuffin = Convert.ToInt32(mainDisplayWidth) / 256;

            for (int i = 0; i < numeroMuffin; i++)
            {

                var paolo = new MR.Gestures.Image   //il cupcake paolo
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Source = ImageSource.FromResource("fondomerende.image.cup_cake_128x128.png"),
                    Opacity = 0.6,
                    Scale = 1,
                };

                Paolo.Children.Add(paolo);

                Anima(paolo);
            }

        }

        private async Task Anima(MR.Gestures.Image sender)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            double casualWidth;
            double spawncasuale;
            double randomRotation;
            uint randomTime;

            var ScreenHeight = App.Current.MainPage.Height;
            var ScreenWidth = App.Current.MainPage.Width;


            //await sender.TranslateTo(0, -ScreenHeight / 2, 0);
            //await sender.TranslateTo(0, ScreenHeight, 10000);


            for (int f = 0; f < 20; f++)
            {

                casualWidth = random.Next(0, Convert.ToInt32(ScreenWidth));
                casualWidth -= ScreenWidth / 2;


                spawncasuale = random.Next(0, Convert.ToInt32(ScreenWidth));
                spawncasuale -= ScreenWidth / 4;

                randomTime = Convert.ToUInt32(random.Next(15000, 20000));

                randomRotation = random.Next(180, 1080);

                await sender.RotateTo(0);
                await sender.TranslateTo(spawncasuale, 0, 0);

                await Task.WhenAny<bool>

                (
                    sender.RotateTo(randomRotation, randomTime),
                    sender.TranslateTo(casualWidth, ScreenHeight, randomTime)
                );


            }
        }

        private void Stack_LongPressed(object sender, LongPressEventArgs e)
        {
            SnackDataDTO index = null;
            foreach (var item in (sender as MR.Gestures.StackLayout).Children)
            {
                if (item is MR.Gestures.Label)
                {
                    var snackName = (item as MR.Gestures.Label).Text;
                    index = result.data.snacks.Single(obj => obj.friendly_name == snackName);
                    break;
                }

            }
            if (index != null)
            {
                string preferito = "";

                foreach (var app in (sender as MR.Gestures.StackLayout).Children)
                {
                    if (app is RoundedCornerView)
                    {
                        foreach (var an in (app as RoundedCornerView).Children)
                        {
                            if (an is AnimationView)
                            {
                                AnimationView ap = (AnimationView)an;

                                if (ap.Animation == "LoadingEating.json")
                                {
                                    ap.Speed = -13f;
                                }
                            }
                        }
                    }
                }
            }
            eatLoading = 0;
        }

        private async void Stack_LongPressing(object sender, LongPressEventArgs e)
        {
            bool verifica = false;
            eatLoading = 0;
            SnackDataDTO index = null;
            foreach (var item in (sender as MR.Gestures.StackLayout).Children)
            {
                if (item is MR.Gestures.Label)
                {
                    var snackName = (item as MR.Gestures.Label).Text;
                    index = result.data.snacks.Single(obj => obj.friendly_name == snackName);
                    break;
                }

            }
            if (index != null)
            {
                string preferito = "";

                foreach (var app in (sender as MR.Gestures.StackLayout).Children)
                {
                    if (app is RoundedCornerView)
                    {
                        foreach (var an in (app as RoundedCornerView).Children)
                        {
                            if (an is AnimationView)
                            {
                                AnimationView ap = (AnimationView)an;

                                if (ap.Animation == "LoadingEating.json")
                                {
                                    ap.IsVisible = true;
                                    ap.FadeTo(1);
                                    ap.Speed = 9.5f;
                                    ap.Play();
                                    ap.OnFinish += async (s, d) =>
                                    {
                                        await Stack_LongFinish(ap, index);
                                    };
                                }
                            }
                        }
                    }
                }
            }
        }

        private async Task Stack_LongFinish(object sender, SnackDataDTO index)
        {
            eatLoading = -1;
            if ((sender as AnimationView).Speed > 0)
            {
                (sender as AnimationView).FadeTo(0, 300);
                EatDTO response = await snackServiceManager.EatAsync(index.id, 1);
                // refresh 
                MessagingCenter.Send(new EditUserViewCell()
                {

                }, "RefreshUF");

                if (response.response.success == true)
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        DependencyService.Get<HapticFeedbackGen>().HapticFeedbackGenSuccessAsync();
                    }
                    else
                    {
                        Vibration.Vibrate(40);
                        await Task.Delay(20);
                        Vibration.Vibrate(40);
                    }
                }
                else
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        DependencyService.Get<HapticFeedbackGen>().HapticFeedbackGenErrorAsync();
                    }
                    else
                    {
                        Vibration.Vibrate(80);
                    }
                }
            }
        }
    }
}