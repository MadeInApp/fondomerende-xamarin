using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fondomerende.Main.Login.PostLogin.AllSnacks.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.About_and_UserSettings.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.LogOut.View;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Settaggio.View;
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
        bool paoloBool = Preferences.Get("Paolo",false);
        int eatLoading = 0;
        public static bool EnablePacman;
        public static string selectedItemBinding { get; set; }
        SnackServiceManager snackServiceManager = new SnackServiceManager();
        List<SnackDataDTO> AllSnacks = new List<SnackDataDTO>();
        SnackDTO result;
        Dictionary<string, int> numerotocchi = new Dictionary<string, int>();
        bool switchStar = false;
        bool swapped = false;
        AnimationView Swap;
        bool miserve = false;

        string previousFavourite;

        object[] Snackarray = new object[100];
        object[] SnackFavarray;

        public AllSnacksPage()
        {
            numerotocchi.Add("numero", 0);
            InitializeComponent();


            GetSnacksMethod(false, false);
            GetSnacksMethod(false, true);
            CreateFavouritesLabel();

            previousFavourite = Preferences.Get("Favourites", "");

            Fade();
            animation();

            MessagingCenter.Subscribe<AllSnacksPage>(this, "PaoloStart", async (arg) =>
            {
                paoloBool = Preferences.Get("Paolo", false);
                animation();
            });

            MessagingCenter.Subscribe<AllSnacksPage>(this, "RefreshGetSnacks", async (arg) =>
            {
                GetSnacksMethod(true, false);
            });




            switch (Device.RuntimePlatform)   //                                              ||\\
            {              //                                                                 || \\                                    
                                                                   //                         ||  \\ Se il dispositivo è Android non mostra la Top Bar della Navigation Page,
                case Device.Android: //                                             \\        ||   \\   Se è iOS invece si (perchè senza è una schifezza)
                    NavigationPage.SetHasNavigationBar(this, false);//                \\      ||    \\        \                
                    break;     //                                                      ||||||||||||||||\/\/|    |
                                                                                      //      ||    //        /       
                default:                                                            //        ||   //
                    NavigationPage.SetHasNavigationBar(this, true);//                         ||  //
                    break;  //                                                                || //
            }               //                                                                ||//
                                                                                         

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

            MessagingCenter.Send(new ChronologyViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new FondoFondoMerendeViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new EditSnackViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new AddSnackViewCell()
            {

            }, "Refresh2");

            MessagingCenter.Send(new BuySnackViewCell()
            {

            }, "Refresh");

            MessagingCenter.Send(new LogoutViewCell()
            {

            }, "Refresh");

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

                    double boxAltezza = 140;

                    //aggiunto ora dopo va visto//
                    double boxLarghezza = 100;
                    var imageButton = new ImageButton
                    {
                        Margin = new Thickness(0, 20, 0, 20),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Scale = 2.6,
                        BackgroundColor = Color.White,
                        InputTransparent = true,
                        Source = "http://fondomerende.madeinapp.net/api/getphoto.php?name=" + result.data.snacks[i].friendly_name,
                    };


                    var StackLayout = new MR.Gestures.StackLayout
                    {
                        WidthRequest = boxAltezza,
                        HeightRequest = boxLarghezza,
                        BackgroundColor = Color.White,
                        InputTransparent = true,
                    };


                    var BordiSmussatiAndroid = new RoundedCornerView
                    {
                        HeightRequest = boxAltezza,
                        WidthRequest = boxLarghezza,
                        RoundedCornerRadius = boxAltezza / 2,
                        BorderColor = c.GetRandomColor(),
                        BorderWidth = 3,
                        InputTransparent = true,
                    };

                    var BordiSmussatiiOS = new RoundedCornerView
                    {
                        HeightRequest = boxAltezza,
                        WidthRequest = boxLarghezza,
                        RoundedCornerRadius = boxAltezza / 4,
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
                    starAnimation.OnFinish -= StopAnimation;
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

        public async Task refreshFavAsync(bool app)
        {
            if (previousFavourite != Preferences.Get("Favourites", ""))
            {
                EmptyStackFav.FadeTo(0, 0); // nasconde la scritta aggiungi snack
                previousFavourite = Preferences.Get("Favourites", "");
                ScrollFavourites.IsVisible = true;
                Column0Fav.Children.Clear();
                Column1Fav.Children.Clear();

                GetSnacksMethod(false, true);
            }

            if(app) HideLabel();

        }

        private async Task HideLabel()
        {
            await Task.Delay(500);
            if (Column0Fav.Children.Count == 0 && Column1Fav.Children.Count == 0)
            {
                EmptyStackFav.FadeTo(0.5, 1000);
                ScrollFavourites.IsVisible = false;
            }
        }

        public void CreateFavouritesLabel()
        {
            var testo = new FormattedString();

            testo.Spans.Add(new Span { Text = "Aggiungi nuovi ", TextColor = Color.Black });
            testo.Spans.Add(new Span { Text = "Preferiti ", TextColor = Color.FromHex("#ffb121") });

            var label = new MR.Gestures.Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 20,
                FormattedText = testo,
            };


            EmptyStackFav.Children.Add(label);
            EmptyStackFav.FadeTo(0);
        }

        public async Task refreshSnackAsync()
        {
            if (previousFavourite != Preferences.Get("Favourites", ""))
            {
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
                await refreshFavAsync(true);
                swapped = false;

                ScrollSnackView.IsVisible = false;
                ScrollFavourites.IsVisible = true;
                ListView.IsVisible = false;
                favourite.Source = ImageSource.FromResource("fondomerende.image.star_fill.png");
                ListToGrid.BackgroundColor = Color.Transparent;

            }
            else
            {
                await refreshSnackAsync();
                refreshFavAsync(false);
                EmptyStackFav.FadeTo(0, 0);
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
                selectedItemBinding = (e.SelectedItem as SnackDataDTO).friendly_name;
                if(EnablePacman)
                {
                    MessagingCenter.Send(new SnackViewCell()
                    {

                    }, "Animate");
                }
                else
                {
                    GetSnacksMethod(true,false) ;
                }
                MessagingCenter.Send(new EditUserViewCell()
                {   

                }, "RefreshUF");


              
            }
            else
            {
                GetSnacksMethod(true, false);
            }
        }

        private async void Fade()
        {
            await StackSnack.FadeTo(0, 0);
            await StackSnack.FadeTo(1, 1400);
        }

        private void Swap_Clicked(object sender, EventArgs e)
        {
            swapped = !swapped;
            if (swapped)
            {
                switchStar = false;
                ListToGrid.BackgroundColor = Color.FromHex("#f29e17");
                ScrollSnackView.IsVisible = false;
                ListView.IsVisible = true;
                EmptyStackFav.FadeTo(0, 0);

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
            if(paoloBool)
            {
                Paolo.Opacity = 100;
                for (int i = 0; i < numeroMuffin; i++)
                {

                    var paolo = new MR.Gestures.Image   //il cupcake paolo
                    {
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        Source = ImageSource.FromResource("fondomerende.image.cup_cake_128x128.png"),
                        Opacity = 0.2,
                        Scale = 1,
                    };

                        Paolo.Children.Add(paolo);
                        Anima(paolo);
               
                }
            }
            else
            {
                Paolo.Opacity = 0;
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
            try
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
                                           
                                        if (ap.Animation == "LoadingEating.json")
                                        {

                                            switch (Device.RuntimePlatform)
                                            {

                                                case Device.Android:
                                                    ap.Speed = -13f;
                                                    break;

                                                case Device.iOS:
                                                    ap.FadeTo(0, 10);
                                                    ap.Speed = -50f;
                                                    break;
                                            }


                                        }
                                    }
                                }
                            }
                        }
                    }
                    eatLoading = 0;
                }
            }catch(Exception Ex)
            {
                
            }
        }

        private async void Stack_LongPressing(object sender, LongPressEventArgs e)
        {
            try
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

                                        ap.ScaleTo(1);
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
            catch (Exception Ex)
            {
                await DisplayAlert("Fondo Merende", "Snack Esaurito!", "Ok");
            }
        }

        private async Task Stack_LongFinish(object sender, SnackDataDTO index)
        {
            
                eatLoading = -1;
                if ((sender as AnimationView).Speed > 0)
                {
                    (sender as AnimationView).Speed = 0;
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
                            await Task.Delay(100);
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
