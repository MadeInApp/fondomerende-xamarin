using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static Android.Content.Res.Resources;

namespace fondomerende.Main.Utilities
{
    class PopupUtilities
    {

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

        public static RoundedCornerView PopUpPiccoli()
        {
            double Altezza = 600;
            double Larghezza = GetLarghezzaPagina() - 80;
            double banner = 50;

            var Round = new RoundedCornerView  //coso che stonda
            {
                RoundedCornerRadius = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = Altezza,
                WidthRequest = Larghezza,
            };

            var stackFondoAndroid = new StackLayout() // 
            {
                HeightRequest = banner,
                WidthRequest = Larghezza,
                BackgroundColor = GetPrimaryAndroidColor(),
            };

            var stackFondoiOS = new StackLayout()
            {
                HeightRequest = banner,
                WidthRequest = Larghezza,
                BackgroundColor = Color.Orange,
            };

            var fondomerende = new Label
            {
                Text = "Fondo merende",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
            };

            var stackBody = new StackLayout
            {
                HeightRequest = Altezza,
                WidthRequest = Larghezza,
                BackgroundColor = Color.White,
            };

            var stackBottoni = new StackLayout
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = Larghezza,
                HeightRequest = banner,
                MinimumHeightRequest = banner,
            };

            var griglia = new Grid
            {

            };

            var buttonCancel = new Button
            {
                Text = "Annulla",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Transparent,
            };

            var buttonConfirm = new Button
            {
                Text = "Conferma",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Transparent,
            };
            stackBottoni.Children.Add(griglia);
            griglia.Children.Add((buttonCancel)); //inzia nella prima colonna
            griglia.Children.Add((buttonConfirm)); //inizia secondsa colonna
            

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    stackFondoAndroid.Children.Add(fondomerende);
                    stackBody.Children.Add(stackFondoAndroid);
                    break;
                default:
                    stackFondoAndroid.Children.Add(fondomerende);
                    stackBody.Children.Add(stackFondoiOS);
                    break;
            }

            stackBody.Children.Add(stackBottoni);
            Round.Children.Add(stackBody);

            return Round;

        }
    }
}
