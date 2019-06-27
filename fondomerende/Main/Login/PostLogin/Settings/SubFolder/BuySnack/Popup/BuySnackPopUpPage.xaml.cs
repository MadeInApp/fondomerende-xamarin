using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.Page;
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
        LineEntry lineAndroid, lineiOs;
        double Altezza, Larghezza;
        RoundedCornerView Round;
        StackLayout stackBody;
        LineEntry prezzoAndroid, prezzoiOs;
        LineEntry scadenzaAndroid, scadenzaiOs;
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
            Altezza = (GetAltezzaPagina() * 30) / 100;
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
            lineAndroid = new LineEntry
            {
                Placeholder = "Quanti snack vuoi acquistare?",
                WidthRequest = 250,
                Keyboard = Keyboard.Numeric,
                Margin = new Thickness(0, 0, 0, -30),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                
            };

            prezzoAndroid = new LineEntry
            {
                Placeholder = "Inserire il prezzo",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 250,
                IsVisible = false,
                Margin = new Thickness(0, 10, 0, 10),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            scadenzaAndroid = new LineEntry
            {
                Placeholder = "Inserire la scadenza",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 250,
                Margin = new Thickness(0, 10, 0, 10),
                IsVisible = false,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };



            lineiOs = new LineEntry
            {
                Placeholder = "Quanti snack vuoi acquistare?",
                WidthRequest = 250,
                Margin = new Thickness(0,10,0,-30),
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,

            };

            prezzoiOs = new LineEntry
            {
                Placeholder = "Inserire il prezzo",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 250,
                Margin = new Thickness(0, 10, 0, 10),
                IsVisible = false,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            scadenzaiOs = new LineEntry
            {
                Placeholder = "Inserire la scadenza",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 250,
                Margin = new Thickness(0, 10, 0, 10),
                IsVisible = false,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };


            


            stackBody = new StackLayout  //stack principale dove è contenuto l'interno di tutto (tranne round che stonda)

            {
                Spacing = 10,
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
                Source = ImageSource.FromResource("fondomerende.image.Edit_Icon_32x32.png"),
                CornerRadius = 20,
                Scale = 1.5,
                BackgroundColor = Color.Transparent,
                Margin = new Thickness (0,0,15,0),
                Aspect = Aspect.AspectFit,
            };

            

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
                    immagine.Clicked += Immagine_ClickedAndroid;
                    prezzoAndroid.TextChanged += EntrataPrezzoAndroid;
                    buttonCancel.Clicked += Discard_Clicked;
                    buttonConfirm.Clicked += Apply_ClickedAndroid;
                    stackBody.Children.Add(lineAndroid);
                    stackBody.Children.Add(prezzoAndroid);
                    stackBody.Children.Add(scadenzaAndroid);

                    stackBody.Children.Add(stackBottoni);
                    stackBody.Children.Add(stackFondoAndroid);
                    Round.Children.Add(stackBody);
                    break;


                case Device.iOS:
                    stackFondoiOS.Children.Add(fondomerende);
                    stackFondoiOS.Children.Add(immagine);
                    immagine.Clicked += Immagine_ClickediOs;
                    prezzoAndroid.TextChanged += EntrataPrezzoiOs;
                    buttonCancel.Clicked += Discard_Clicked;
                    buttonConfirm.Clicked += Apply_ClickediOs;
                    stackBody.Children.Add(lineiOs);
                    stackBody.Children.Add(prezzoiOs);
                    stackBody.Children.Add(scadenzaiOs);

                    stackBody.Children.Add(stackBottoni);
                    stackBody.Children.Add(stackFondoiOS);
                    Round.Children.Add(stackBody);

                    break;
            }
          //  entry.TextChanged += Entrata;


            
            

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
            MessagingCenter.Send(new BuySnackListPage()
            {

            }, "Close");
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
            return base.OnBackButtonPressed();
        }
        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
 
            return base.OnBackgroundClicked();
        }


        private async void Immagine_ClickedAndroid(object sender, EventArgs e)
        {
            swap = true;
            Altezza = (GetAltezzaPagina()*40)/100;
            Round.HeightRequest = Altezza;
            stackBody.Spacing = 20;
            immagine.IsVisible = false;
            prezzoAndroid.IsVisible = true;
            scadenzaAndroid.IsVisible = true;
        }

        private async void Immagine_ClickediOs(object sender, EventArgs e)
        {
            swap = true;
            Altezza = (GetAltezzaPagina() * 40) / 100;
            Round.HeightRequest = Altezza;
            stackBody.Spacing = 20;
            immagine.IsVisible = false;
            prezzoiOs.IsVisible = true;
            scadenzaiOs.IsVisible = true;
        }


        public void EntrataPrezzoAndroid(object sender, TextChangedEventArgs e)
        {
            if (prezzoAndroid.CursorPosition == 1 && isDone)
            {
                if (prezzoAndroid.Text.Substring(1, 1) == ",")
                {
                    prezzoAndroid.MaxLength = 4;
                    isDone = false;
                }
                else
                {
                    prezzoAndroid.MaxLength = 5;
                    prezzoAndroid.Text = prezzoAndroid.Text + ",";
                    isDone = false;
                }
            }

            if (prezzoAndroid.CursorPosition == 0)
            {
                isDone = true;
            }
        }
        public void EntrataPrezzoiOs(object sender, TextChangedEventArgs e)
        {
            if (prezzoiOs.CursorPosition == 1 && isDone)
            {
                if (prezzoiOs.Text.Substring(1, 1) == ",")
                {
                    prezzoiOs.MaxLength = 4;
                    isDone = false;
                }
                else
                {
                    prezzoiOs.MaxLength = 5;
                    prezzoiOs.Text = prezzoiOs.Text + ",";
                    isDone = false;
                }
            }

            if (prezzoiOs.CursorPosition == 0)
            {
                isDone = true;
            }
        }



        private async void Apply_ClickedAndroid(object sender, EventArgs e)
        { 
            SnackServiceManager snackService = new SnackServiceManager();
            if (swap == false)
            {
                if (lineAndroid.Text == null || lineAndroid.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci la quantità", "OK");
                }
                else
                {
                    var result = await snackService.BuySnackAsync(BuySnackListPage.SelectedSnackID, Convert.ToInt32(lineAndroid.Text));
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
                if (lineAndroid.Text == null || lineAndroid.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci la quantità", "OK");
                }
                if(prezzoAndroid.Text == null || prezzoAndroid.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci il prezzo", "OK");
                }
                if(scadenzaAndroid.Text == null ||  scadenzaAndroid.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci i giorni di scadenza", "OK");
                }
                else
                {
                    var result = await snackService.BuySnackAsync2(BuySnackListPage.SelectedSnackID, Int32.Parse(lineAndroid.Text),prezzoAndroid.Text, scadenzaAndroid.Text);
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


        private async void Apply_ClickediOs(object sender, EventArgs e)
        {
            SnackServiceManager snackService = new SnackServiceManager();
            if (swap == false)
            {
                if (lineiOs.Text == null || lineiOs.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci la quantità", "OK");
                }
                else
                {
                    var result = await snackService.BuySnackAsync(BuySnackListPage.SelectedSnackID, Convert.ToInt32(lineiOs.Text));
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
            if (swap == true)
            {
                if (lineiOs.Text == null || lineiOs.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci la quantità", "OK");
                }
                if (prezzoiOs.Text == null || prezzoiOs.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci il prezzo", "OK");
                }
                if (scadenzaiOs.Text == null || scadenzaiOs.Text == "")
                {
                    await DisplayAlert("Fondo Merende", "Inserisci i giorni di scadenza", "OK");
                }
                else
                {
                    var result = await snackService.BuySnackAsync2(BuySnackListPage.SelectedSnackID, Int32.Parse(lineiOs.Text), prezzoiOs.Text, scadenzaiOs.Text);
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
            await Navigation.PopAllPopupAsync();
        }
    }
}