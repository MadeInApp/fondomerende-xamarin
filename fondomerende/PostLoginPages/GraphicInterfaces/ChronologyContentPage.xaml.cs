using fondomerende.Services.RESTServices;
using Java.Util.Prefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using Xamarin.Essentials;

namespace fondomerende.PostLoginPages.GraphicInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChronologyContentPage : ContentPage
    {
        private double diametro = 50;
        private double larghezzaLinea = 4;
        private double altezzaLinea = 30;
        private int posizione = 3;
        Color colore;
        Dictionary<string, Color> colorByName = new Dictionary<string, Color>();
        Dictionary<string, double> sizeByName = new Dictionary<string, double>();

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
            SizeFrame();
            Aspetta();
        }



        private void Fusione()
        {
            int i = 0;
            do
            {
                AddAction(0);

                    if(cronologia[i + 1] == null)  AddTimeLine(diametro, larghezzaLinea, altezzaLinea);

                i++;
            }while (cronologia[i + 1] == null);
        }

        private void AddAction(int posizione)
        {
            string dataLabel = "";
            string[] strSplit = cronologia[posizione].Split();           
            ColorBack(strSplit[2]);

            for (int i = 2; i <= strSplit.Length; i++)
            {
                dataLabel = " "+strSplit[i];
            }

            var stackPrincipale = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            var cerchio = new RoundedCornerView
            {
                HeightRequest = diametro + (diametro * sizeByName[strSplit[2]]),
                WidthRequest = diametro + (diametro * sizeByName[strSplit[2]]),
                MinimumHeightRequest = diametro + (diametro * sizeByName[strSplit[2]]),
                MinimumWidthRequest = diametro + (diametro * sizeByName[strSplit[2]]),
                RoundedCornerRadius = diametro+(diametro*sizeByName[strSplit[2]])
            };

            var stackLabel = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = colorByName[strSplit[2]]
            };

            var firstLetter = new Label
            {
                Text = First_letter(cronologia[posizione]),
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
                Text = dataLabel,
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
            string[] strSplit = cronologia[posizione].Split();
            double paddingLinea = (diametro + (diametro * sizeByName[strSplit[2]]) )/ 2 - larghezzaLinea/2;
            

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
                Margin = new Thickness(paddingLinea, 0, 0, 0),
                BackgroundColor = colorByName[strSplit[2]]

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

        public string First_letter(string app)        //Grafica
        {
            string firstLetter = "";

            
            firstLetter = (app.Substring(0, 1));
            return firstLetter;
        }

        public void ColorBack(string app)
        {
            ColorRandom c = new ColorRandom();
            Color colorapp;
          
            colorapp = c.GetRandomColor();
           

            if((app).Equals(Xamarin.Essentials.Preferences.Get("friendly-name", " ")))
            {
                colorapp = Color.FromHex(Xamarin.Essentials.Preferences.Get("Colore", "#000000"));
                colorByName.Add(app, colorapp);
            }

            if (!colorByName.ContainsKey(app))
            {
                colorByName.Add(app, colorapp);
            }
        }

        public void SizeFrame()
        {
            for(int i = 0; i <= cronologia.Length; i++)
            {
                string[] strSplit = cronologia[i].Split();

                if (!sizeByName.ContainsKey(strSplit[2]))
                {
                    sizeByName.Add(strSplit[2], 0.11);  //grandezza incrementata 
                }
                else
                {
                    sizeByName[strSplit[2]] = +0.11;
                }
            }
        }

        public async Task GetLastActions()
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

        public async void Aspetta()
        {
            await GetLastActions();
            AddAction(0);
            AddTimeLine(diametro, larghezzaLinea, altezzaLinea);
            AddAction(11);
            AddTimeLine(diametro, larghezzaLinea, altezzaLinea);
        }
    }
}