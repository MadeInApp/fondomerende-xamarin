using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.PostLoginPages.GraphicInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChronologyContentPage : ContentPage
    {
        private double diametro = 50;
        private double larghezzaLinea = 4;
        private double altezzaLinea = 50;

        string[] cronologia;
        public ChronologyContentPage()
        {
            InitializeComponent();
            switch (Device.RuntimePlatform)             //Se il dispositivo è Android non mostra la Top Bar della Navigation Page, se è iOS la mostra
            {
                default:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;

            }
            AddAction();
            AddTimeLine(diametro, larghezzaLinea, altezzaLinea );
            AddAction();
            AddTimeLine(diametro, larghezzaLinea, altezzaLinea);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void Fusione()
        {
            int i = 0;
            do
            {
                AddAction();

                    if(cronologia[i + 1] == null)  AddTimeLine(diametro, larghezzaLinea, altezzaLinea);

                i++;
            }while (cronologia[i + 1] == null);
        }

        private void AddAction()
        {
            var stackPrincipale = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            var cerchio = new RoundedCornerView
            {
                HeightRequest = diametro,
                WidthRequest = diametro,
                MinimumHeightRequest = diametro,
                MinimumWidthRequest = diametro,
                BackgroundColor = Color.Wheat,
                RoundedCornerRadius = diametro/2
            };

            var stackLabel = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Red
            };

            var firstLetter = new Label
            {
                Text="C",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };

            var textAction = new Label
            {
                Text = "Ha mangiato",
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
            };

            cerchio.Children.Add(stackLabel);
            stackLabel.Children.Add(firstLetter);
            stackPrincipale.Children.Add(cerchio);
            stackPrincipale.Children.Add(textAction);
            
            ContentLayout.Children.Add(stackPrincipale);
        }

        public void AddTimeLine(double diametro, double larghezzaLinea,double altezzaLinea)
        {
            double paddingLinea = diametro/2 - larghezzaLinea/2;
            var stackPrincipale = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Horizontal,

            };

            var linea = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = altezzaLinea,
                WidthRequest = larghezzaLinea,
                Margin = new Thickness(paddingLinea,0,0,0),
                BackgroundColor = Color.White,
                
            };

            var orario = new Label
            {
                Text = "1h 37min",
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
            };

            stackPrincipale.Children.Add(linea);
            stackPrincipale.Children.Add(orario);


            ContentLayout.Children.Add(stackPrincipale);
        }

        //public void First_letter()        //Grafica
        //{
        //    string firstLetter = ;

        //    string[] strSplit = //array da passare .Split();

        //    foreach (string res in strSplit)
        //    {
        //        firstLetter = (res.Substring(0, 1));
        //    }
        //    inizialeLabel.Text = firstLetter;
        //}
        public async void GetLastActions()
        {
            LastActionServiceManager lastAction = new LastActionServiceManager();
            var result = await lastAction.GetLastActions();

            if (result.response.success == true)
            {
                cronologia = result.data.actions;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}