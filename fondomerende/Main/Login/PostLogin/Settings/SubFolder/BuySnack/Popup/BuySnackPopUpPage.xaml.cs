using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page;
using fondomerende.Main.Services.RESTServices;
using fondomerende.Main.Utilities;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Popup


{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        bool OneClick = false;
        ImageButton immagine;
        LineEntry line;
        double Altezza, Larghezza;
        RoundedCornerView Round;
        StackLayout stackBody;
        LineEntry prezzo;
        LineEntry scadenza;
        Button buttonCancel, buttonConfirm;
        bool isDone, swap=false;
        string appoggio;
        
        SnackServiceManager snackService = new SnackServiceManager();
        public BuySnackPopUpPage()
        {
            InitializeComponent();
            PopupBuy();
        }

        public static Color GetPrimaryAndroidColor()
        {
            return Color.FromHex("#f29e17");
        }

        public static double GetLarghezzaPagina()
        {
            return App.Current.MainPage.Width;
        }

        public static double GetAltezzaPagina()
        {
            return App.Current.MainPage.Height;
        }
        private void PopupBuy()
        {
            Altezza = 200;
            Larghezza = GetLarghezzaPagina() - 80;
            double banner = 50;

            Round = new RoundedCornerView  //coso che stonda
            {
                RoundedCornerRadius = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = Altezza,
                WidthRequest = Larghezza,
            };

            var stackFondoAndroid = new StackLayout() //per android 
            {
                HeightRequest = banner,
                WidthRequest = Larghezza,
                BackgroundColor = GetPrimaryAndroidColor(),
                Orientation = StackOrientation.Horizontal,
            };

            var stackFondoiOS = new StackLayout()  //per ios 
            {
                HeightRequest = banner,
                WidthRequest = Larghezza,
                BackgroundColor = Color.Orange,
                Orientation = StackOrientation.Horizontal,
            };

            var fondomerende = new Label  //Label per Il titolo banner 
            {
                Text = "Fondo merende",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
            };
            line = new LineEntry
            {
                Placeholder = "Quanti snack vuoi acquistare?",
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                
            };

            prezzo = new LineEntry
            {
                Placeholder = "Inserire il prezzo",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 250,
                IsVisible = false,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            scadenza = new LineEntry
            {
                Placeholder = "Inserire la scadenza",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 250,
                IsVisible = false,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };


            prezzo.TextChanged += EntrataPrezzo;


            stackBody = new StackLayout  //stack principale dove è contenuto l'interno di tutto (tranne round che stonda)

            {
                HeightRequest = Altezza,
                WidthRequest = Larghezza,
                BackgroundColor = Color.White,
            };

            var stackBottoni = new StackLayout  //stack che contiene la gridlia dei bottoni
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = Larghezza,
                HeightRequest = banner,
                MinimumHeightRequest = banner,
            };

            var griglia = new Grid //griglia che contiene i bottoni
            {

            };


            immagine = new ImageButton
            {
                Source = ImageSource.FromResource("fondomerende.image.settings_icon_64x64.png"),
                CornerRadius = 20,
                Scale = 1,
                BackgroundColor = Color.Transparent,
                Margin = new Thickness (0,0,15,0),
                Aspect = Aspect.AspectFit,
            };

            immagine.Clicked += Immagine_Clicked;

            buttonCancel = new Button
            {
                Text = "Annulla",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Transparent,
            };

             buttonConfirm = new Button
            {
                Text = "Conferma",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Transparent,
            };

            stackBottoni.Children.Add(griglia);
            griglia.Children.Add((buttonCancel)); //inzia nella prima colonna
            griglia.Children.Add((buttonConfirm)); //inizia seconda colonna

            Grid.SetColumn(buttonCancel, 0); //mi è toccato farlo qui
            Grid.SetColumn(buttonConfirm, 1);



            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    stackFondoAndroid.Children.Add(fondomerende);
                    stackFondoAndroid.Children.Add(immagine);
                    stackBody.Children.Add(stackFondoAndroid);
                    break;
                default:
                    stackFondoAndroid.Children.Add(fondomerende);
                    stackFondoAndroid.Children.Add(immagine);
                    stackBody.Children.Add(stackFondoiOS);
                    break;
            }
          //  entry.TextChanged += Entrata;


            buttonCancel.Clicked += Discard_Clicked;
            buttonConfirm.Clicked += Apply_Clicked;

            
            stackBody.Children.Add(line);
            stackBody.Children.Add(prezzo);
            stackBody.Children.Add(scadenza);

            stackBody.Children.Add(stackBottoni);
            Round.Children.Add(stackBody);

            PopupBuySnack.Content = Round;
        }

        public void Entrata(object sender, TextChangedEventArgs e)
        {
            appoggio = e.NewTextValue;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Send(new BuySnackListPage()
            {

            }, "Refresh");
               
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            base.OnBackButtonPressed();
            return false;
        }
        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
 
            return base.OnBackgroundClicked();
        }


        private async void Immagine_Clicked(object sender, EventArgs e)
        {
            swap = true;
            Altezza = GetAltezzaPagina() / 2.2;
            Round.HeightRequest = Altezza;
            immagine.IsVisible = false;
            prezzo.IsVisible = true;
            scadenza.IsVisible = true;
        }


        public void EntrataPrezzo(object sender, TextChangedEventArgs e)
        {
            if (prezzo.CursorPosition == 1 && isDone)
            {
                if (prezzo.Text.Substring(1, 1) == ",")
                {
                    prezzo.MaxLength = 4;
                    isDone = false;
                }
                else
                {
                    prezzo.MaxLength = 5;
                    prezzo.Text = prezzo.Text + ",";
                    isDone = false;
                }
            }

            if (prezzo.CursorPosition == 0)
            {
                isDone = true;
            }
        }



        private async void Apply_Clicked(object sender, EventArgs e)
        { 
            SnackServiceManager snackService = new SnackServiceManager();
            if (swap == false)
            {
                if (line.Text == null || line.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci la quantità", "OK");
                }
                else
                {
                    var result = await snackService.BuySnackAsync(BuySnackListPage.SelectedSnackID, Convert.ToInt32(line.Text));
                    if (result != null)
                    {
                        if (result.response.success)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Fondo Merende", result.response.message, "Ok");
                        }
                    }
                    else
                    {

                    }
                }
            }
            if(swap == true )
            {
                if (line.Text == null || line.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci la quantità", "OK");
                }
                if(prezzo.Text == null || prezzo.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci il prezzo", "OK");
                }
                if(scadenza.Text == null ||  scadenza.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci i giorni di scadenza", "OK");
                }
                else
                {
                    var result = await snackService.BuySnackAsync2(BuySnackListPage.SelectedSnackID, Int32.Parse(line.Text),prezzo.Text, scadenza.Text);
                    if (result != null)
                    {
                        if (result.response.success)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Fondo Merende", result.response.message, "Ok");
                        }
                    }
                }

            }
           
        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            Discard.IsEnabled = false;
            await Navigation.PopPopupAsync();
        }
    }
}