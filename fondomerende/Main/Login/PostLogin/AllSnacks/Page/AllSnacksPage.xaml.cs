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
using Lottie.Forms;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View;
using fondomerende.Main.Login.PostLogin.AllSnacks.View;

namespace fondomerende.Main.Login.PostLogin.AllSnack.Page
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllSnacksPage : ContentPage
    {
        double priceBinding;
        public static string selectedItemBinding { get; set; }
        SnackServiceManager snackServiceManager = new SnackServiceManager();
        List<SnackDataDTO> AllSnacks = new List<SnackDataDTO>();
        SnackDTO result;
        Dictionary<string, int> numerotocchi = new Dictionary<string, int>();
        bool switchStar = false;
        AnimationView Swap;
        public AllSnacksPage()
        {
            numerotocchi.Add("numero", 0);
            InitializeComponent();
            GetSnacksMethod(false,false);
            GetSnacksMethod(false,true);
            Fade();
            animation();
            MessagingCenter.Subscribe<AllSnacksPage>(this, "RefreshGetSnacks", async (arg) =>
            {
                GetSnacksMethod(false,false);
            });




            switch (Device.RuntimePlatform)                                                    //\\
            {                                                                                  // \\                                    
                                                                                               //  \\ Se il dispositivo è Android non mostra la Top Bar della Navigation Page,
                case Device.Android:                                                           //   \\   Se è iOS invece si (perchè senza è una schifezza)
                    NavigationPage.SetHasNavigationBar(this, false);                           //    \\
                    break;                                                                           //
                                                                                                    //
                default:                                                                           //
                    NavigationPage.SetHasNavigationBar(this, true);                               //
                    break;                                                                       //
            }                                                                                   //


            Swap = new AnimationView
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Animation = "list2grid.json",
            };
            Swap.OnClick += Swap_Clicked;
            GridView1.Children.Add(Swap, 0, 0);


            ListView.RefreshCommand = new Command(async () =>                                //
            {                                                                                //         
                await RefreshDataAsync();                                                    //
                ListView.IsRefreshing = false;                                               //
            });

            MessagingCenter.Subscribe<AllSnacksPage>(this, "Animation", async (value) =>
            {
                WalletAnimation();
            });

        }                                                                                         //
        public async Task RefreshDataAsync()                                                    //
        {                                                                                      //
            await GetSnacksMethod(true,false);                                                      //
        }

        public async Task RefreshFavouriteDataAsync()                                                    //
        {                                                                                      //
            await GetSnacksMethod(true, true);                                                      //
        }              

        private void WalletAnimation()
        {
            Wallet.Play();
            Wallet.Speed = 1f;
        }




        public async Task GetSnacksMethod(bool Loaded,bool favourites)     //ottiene la lista degli snack e la applica alla ListView
        {
            result = await snackServiceManager.GetSnacksAsync();
           
            ListView.ItemsSource = result.data.snacks;
            int FavIndex = 0;
            if (!Loaded) //!WORKAROUND!   in questo modo si evita il crash ma la griglia non si aggiorna, urge investigazione sul vero problema
            {
                for (int i = 0; i <= result.data.snacks.Count; i++)
                {
                    bool addfav = false; //variabile di appoggio
                    bool visibilità = true;
                    if (favourites && !Check_Favourites(result.data.snacks[i].id))
                    {
                        addfav = true;
                    }else if(favourites && Check_Favourites(result.data.snacks[i].id))
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

                    string e;
                    if (Check_Favourites(result.data.snacks[i].id))
                    {
                        e = "fondomerende.image.star_empty.png";
                    }
                    else
                    {
                        e = "fondomerende.image.star_fill.png";
                    }

                    var star = new Image
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
                        Scale = 1.3,
                        Loop = false,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        AutoPlay = false,
                        InputTransparent = true,
                        IsVisible = false,
                    };


                    var app = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        IsVisible = visibilità,
                    };

                    starAnimation.OnFinish += StopAnimation;

                    var tgr = new TapGestureRecognizer();
                    tgr.Tapped += Tgr_Tapped;
                    app.GestureRecognizers.Add(tgr);

                    var tgr2 = new TapGestureRecognizer();
                    tgr2.NumberOfTapsRequired = 2;
                    tgr2.Tapped += Tgr2_Tapped;
                    app.GestureRecognizers.Add(tgr2);

                    StackLayout.Children.Add(imageButton);

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:

                            BordiSmussatiAndroid.Children.Add(StackLayout);
                            
                            BordiSmussatiAndroid.Children.Add(starAnimation);
                            BordiSmussatiAndroid.Children.Add(star);

                            app.Children.Add(BordiSmussatiAndroid);

                            break;

                        default:

                            BordiSmussatiiOS.Children.Add(StackLayout);
                            
                            BordiSmussatiiOS.Children.Add(starAnimation);
                            BordiSmussatiiOS.Children.Add(star);

                            app.Children.Add(BordiSmussatiiOS);
                            break;
                    }

                    app.Children.Add(label);

                    if (addfav && favourites)
                    {
                        if (FavIndex % 2 == 0) Column0Fav.Children.Add(app);
                        else Column1Fav.Children.Add(app);

                        FavIndex++;
                    }
                    else
                    {
                        if (i % 2 == 0) Column0.Children.Add(app);
                        else Column1.Children.Add(app);
                    }
                    
                }
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
                
                if(Convert.ToString(id) == strSplit[i])
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

                for (int i=0 ; i < strSplit.Length ; i++)
                {
                    if (strSplit[i] != fav)
                    {
                        if (getfav == "") newpreferiti += strSplit[i];
                        else newpreferiti += strSplit[i] + ",";
                    }
                }
                Preferences.Set("Favourites",newpreferiti);
                return false;
            }
            else //se non lo è
            {
                string concatena = fav + "," + getfav;
                Preferences.Set("Favourites", concatena);
                return true;
            }
        }

        private void Tgr2_Tapped(object sender, EventArgs e)
        {
            SnackDataDTO index = null;
            foreach (var item in (sender as StackLayout).Children)
            {
                if (item is Label)
                {
                    var snackName = (item as Label).Text;
                    index = result.data.snacks.Single(obj => obj.friendly_name == snackName);
                    break;
                }

            }
            if (index != null)
            {
                string preferito="";

                foreach(var app in (sender as StackLayout).Children)
                {
                    if(app is RoundedCornerView)
                    {
                        foreach (var an in (app as RoundedCornerView).Children)
                        {
                            if (an is AnimationView)
                            {
                                AnimationView ap = (AnimationView)an;

                                //per le preferenze 
                                if (Check_FavouritesAndSet(index.id))
                                {
                                    ap.IsVisible = true;
                                    ap.FadeTo(1);
                                    ap.Play();
                                }
                                else
                                {
                                    
                                }

                            }

                            if (an is Image)
                            {
                                Image image = (Image)an;
                                string a="";
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

        private async void favourite_Clicked(object sender, EventArgs e)
        {
            switchStar = !switchStar;
            if(switchStar)
            {
                await RefreshFavouriteDataAsync();
                ScrollSnackView.IsVisible = false;
                ScrollFavourites.IsVisible = true;
                favourite.Source = ImageSource.FromResource("fondomerende.image.star_fill.png");

            }
            else
            {
                await RefreshDataAsync();
                ScrollSnackView.IsVisible = true;
                ScrollFavourites.IsVisible = false;
                favourite.Source = ImageSource.FromResource("fondomerende.image.star_empty.png");
            }

        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)     //quando uno snack è tappato si apre un prompt in cui viene chiesto se lo si vuole mangiare
        {
            var ans = await DisplayAlert("Fondo Merende", "Vuoi davvero mangiare " + (e.SelectedItem as SnackDataDTO).friendly_name + "?", "Si", "No");

            if (ans == true)
            {
                await snackServiceManager.EatAsync((e.SelectedItem as SnackDataDTO).id, 1);
               // await GetSnacksMethod(true);
                MessagingCenter.Send(new EditUserViewCell()
                {

                }, "RefreshUF");


                selectedItemBinding = (e.SelectedItem as SnackDataDTO).friendly_name;

                MessagingCenter.Send(new SnackViewCell()
                {
                    
                }, "Animate");
            }
            else
            {
              //  await GetSnacksMethod(true);
            }
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
                Swap.Play();
                // Swap.Speed = 0.7f;
                Swap.FlowDirection = FlowDirection.LeftToRight;
                ScrollSnackView.IsVisible = false;
                ListView.IsVisible = true;
            }
            else
            {
                Swap.Play();
                Swap.FlowDirection = FlowDirection.RightToLeft;

                ListView.IsVisible = false;
                ScrollSnackView.IsVisible = true;
            }
        }

        private async void WalletClicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new DepositPopUp());
        }

        private async void animation()
        {
            var mainDisplayWidth = DeviceDisplay.MainDisplayInfo.Width;
            var mainDisplayHeight = DeviceDisplay.MainDisplayInfo.Height;
            int numeroMuffin = Convert.ToInt32(mainDisplayWidth)/384;

            for(int i = 0; i< numeroMuffin; i++)
            {
                var paolo = new Image   //il cupcake paolo
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Source = ImageSource.FromResource("fondomerende.image.cup_cake_128x128.png"),
                    Opacity = 0.6,
                    Scale = 1,
                };



                Random randomWidth = new Random((int)DateTime.Now.Ticks);
                double casual;
                double spawncasuale;

                PaoloGrid.Children.Add(paolo);

                Grid.SetColumn(paolo, 0);
                Grid.SetRow(paolo, 0);

                await paolo.TranslateTo(0, -mainDisplayHeight / 2, 0);
                await paolo.TranslateTo(0, mainDisplayHeight, 10000);

                
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

        }

        private void SetFavourite(object sender,EventArgs e)
       {
            //if(Preferences.ContainsKey("Favourite")) => Preferences.Add("Favourite",); ;
            //Preferences.Add("",);
       }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
            
        {
        }
    }

}
