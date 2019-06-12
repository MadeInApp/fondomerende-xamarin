using fondomerende.Main.Services.RESTServices;
using Java.Util.Prefs;
using System;
using FormsControls.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using Xamarin.Essentials;
using fondomerende.Main.Utilities;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChronologyContentPage : AnimationPage
    {
        private double diametro = 40;
        private double larghezzaLinea = 3;
        private double altezzaLinea = 20;

        private double diametroMod;

        Dictionary<string, Color> colorByName = new Dictionary<string, Color>();
        Dictionary<string, double> sizeByName = new Dictionary<string, double>();
        Dictionary<string, string> dateByTime = new Dictionary<string, string>();
        Dictionary<string, string> mangione = new Dictionary<string, string>();

        Dictionary<string, string> traduttore = new Dictionary<string, string>();
        

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
            Aspetta();
            
        }



        private void Fusione()
        {
            traduttore.Add("added", "ha aggiunto");
            traduttore.Add("ate", "ha mangiato");
            traduttore.Add("bought", "ha comprato");
            traduttore.Add("deposited", "ha depositato");
            mangione.Add("mangione", "");
            dateByTime.Add("Intervallo", "");

            ColorBack();
            SizeFrame();
            AddMangione();

            for (int i=0;i<cronologia.Length;i++)
            {
                AddAction(i);
                if(i!=cronologia.Length-1)  AddTimeLine(i);

            }
        }
        public void AddMangione()
        {
            var stackPrincipale = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            var persona = new Label
            {
                Text = "Mangione: " + mangione["mangione"],
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = 5,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex(Xamarin.Essentials.Preferences.Get("Colore", "#000000")),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };

            stackPrincipale.Children.Add(persona);

            ContentLayout.Children.Add(stackPrincipale);

        }
        private void AddAction(int posizione)
        {
            string dataLabel = "";
            string[] strSplit = cronologia[posizione].Split();

            for (int i = 2; i < strSplit.Length; i++)
            {
                if (traduttore.ContainsKey(strSplit[i]))
                {
                    dataLabel = dataLabel + " " + traduttore[strSplit[i]];
                }
                else
                {
                    dataLabel = dataLabel + " " + strSplit[i];
                }               
            }

            var stackPrincipale = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            switch (Device.RuntimePlatform)             //Se il dispositivo è Android non mostra la Top Bar della Navigation Page, se è iOS la mostra
            {
                default:
                    var cerchioiOS = new Frame
                    {
                        HeightRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                        WidthRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                        CornerRadius = Convert.ToSingle(diametro/2),
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        BackgroundColor = colorByName[strSplit[2]],
                    };

                    var stackLabeliOS = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = colorByName[strSplit[2]]
                    };

                    var firstLetteriOS = new Label
                    {
                        Text = First_letter(strSplit[2]),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                    };

                    var textActioniOS = new Label
                    {
                        Text = dataLabel,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 12,
                        Opacity = 0.6,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black,
                        VerticalTextAlignment = TextAlignment.Center,
                    };

                    diametroMod = diametro + (diametro * sizeByName[strSplit[2]]);

                    cerchioiOS.Content = stackLabeliOS;
                    stackLabeliOS.Children.Add(firstLetteriOS);
                    stackPrincipale.Children.Add(cerchioiOS);
                    stackPrincipale.Children.Add(textActioniOS);

                    ContentLayout.Children.Add(stackPrincipale);
                    break;

                case Device.Android:

                    var cerchio = new RoundedCornerView
                    {
                        HeightRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                        WidthRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                        MinimumHeightRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                        MinimumWidthRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                        RoundedCornerRadius = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                        Margin = new Thickness(3, 0, 0, 0),
                        BorderColor = Color.Black,
                        BorderWidth = 3,
                    };

                    var stackLabel = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = colorByName[strSplit[2]]
                    };

                    var firstLetter = new Label
                    {
                        Text = First_letter(strSplit[2]),
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
                        FontSize = 12,
                        Opacity = 0.6,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black,
                        VerticalTextAlignment = TextAlignment.Center,
                    };

                    diametroMod = diametro + (diametro * sizeByName[strSplit[2]]);

                    cerchio.Children.Add(stackLabel);
                    stackLabel.Children.Add(firstLetter);
                    stackPrincipale.Children.Add(cerchio);
                    stackPrincipale.Children.Add(textAction);

                    ContentLayout.Children.Add(stackPrincipale);
                    break;
            }

           
        }

        public void AddTimeLine(int posizione)
        {
            string[] strSplit = cronologia[posizione].Split();
            double paddingLinea = (diametroMod + (diametroMod * sizeByName[strSplit[2]]) )/ 2 - larghezzaLinea/2;
            

            var stackPrincipale = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(3, 0, 0, 0)
            };

            var linea = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = altezzaLinea+ (altezzaLinea * LenghtLine(posizione)),
                WidthRequest = larghezzaLinea,
                Margin = new Thickness(paddingLinea, 0, 0, 0),
                BackgroundColor = colorByName[strSplit[2]],
            };

            var app = new StackLayout { };

            var orario = new Label
            {
                Text = TestoOrario(posizione),
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12,
                Opacity = 0.6,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
            };

            var gradiente = new GradientColorStack
            {
                StartColor = colorByName[strSplit[2]],
                EndColor = LastColor(posizione),
            };


            stackPrincipale.Children.Add(linea);
            stackPrincipale.Children.Add(orario);


            ContentLayout.Children.Add(stackPrincipale);
        }

        public string First_letter(string app)        //Grafica
        {      
            string firstLetter = "";

            
            firstLetter = (app.Substring(0, 1));
            if (app == "added") firstLetter = "@";

            return firstLetter;
        }

        public Color LastColor(int posizione)           //appoggio
        {
            string[] strString = cronologia[posizione + 1].Split();

            return colorByName[strString[2]];
        }

        public void ColorBack()
        {
            ColorRandom c = new ColorRandom();

            for (int i = 0; i < cronologia.Length; i++)
            {
                string[] strSplit = cronologia[i].Split();

                Color colorapp = c.GetRandomColor();

                while (colorapp == Color.FromHex(Xamarin.Essentials.Preferences.Get("Colore", "#000000")))
                {
                    colorapp = c.GetRandomColor();
                }
           
            

                if((strSplit[2]).Equals(Xamarin.Essentials.Preferences.Get("friendly-name", " ")))
                {
                    colorapp = Color.FromHex(Xamarin.Essentials.Preferences.Get("Colore", "#000000"));

                    if(!colorByName.ContainsKey(Xamarin.Essentials.Preferences.Get("friendly-name", " ")))
                    {
                        colorByName.Add(strSplit[2], colorapp);
                    }
                   
                }

                if (!colorByName.ContainsKey(strSplit[2]))
                {
                    colorByName.Add(strSplit[2], colorapp);
                }
            }
        }

        public void SizeFrame()
        {
            double app = 0;
            string app2 = "";
            for(int i = 0; i < cronologia.Length; i++)
            {
                string[] strSplit = cronologia[i].Split();

                if (!sizeByName.ContainsKey(strSplit[2]))
                {
                    sizeByName.Add(strSplit[2], 0.001);  //grandezza incrementata 
                }
                else
                {
                    sizeByName[strSplit[2]] += 0.001;
                }

                if (sizeByName[strSplit[2]] > app)
                {
                    app = sizeByName[strSplit[2]];
                    app2 = strSplit[2];
                }
            }
            mangione["mangione"] = app2;
        }

        public string TestoOrario(int posizione)
        {
            DateTime current = TrimDate(posizione);
            DateTime next = TrimDate(posizione + 1);

            int[] app = new int[5];

            TimeSpan ris = current.Subtract(next);

            double giorni = ris.TotalDays;
            double temp = 0;

            double ore = giorni * 24;
            if (giorni < 1)
            {
                double minuti = ore * 60;
                if (ore < 1)
                {
                    double secondi = minuti * 60;
                    if (minuti < 1)
                    {
                        return (Convert.ToInt32(secondi)) + "s";
                    }
                    else
                    {
                        temp = Math.Abs((minuti - Convert.ToInt32(minuti)) * 60);
                        return Convert.ToInt32(minuti) + "m "+ Convert.ToInt32(temp)+"s";
                    }

                }
                else
                {
                    temp = Math.Abs((ore - Convert.ToInt32(ore)) * 60);
                    return Convert.ToInt32(ore) + "h " + Convert.ToInt32(temp)+"m";
                }

            }
            else
            {
                return Convert.ToInt32(giorni) + "g";               
            }
        }

        public double LenghtLine(int posizione)
        {
            DateTime current = TrimDate(posizione);
            DateTime next = TrimDate(posizione + 1);

            int[] app = new int[5];

            TimeSpan ris = current.Subtract(next);

            double giorni = ris.TotalDays;
            double multipler = 0;

            double ore = giorni * 24;
            if (giorni < 1)
            {
                double minuti = ore * 60;
                if (ore < 1)
                {
                    double secondi = minuti * 60;
                    if (minuti < 1)
                    {
                        multipler += ris.TotalSeconds * 0.0005;
                    }
                    else
                    {
                        multipler += ris.TotalMinutes*0.0013;
                    }
                    
                }
                else
                {                  
                    multipler += ris.TotalHours*0.83;
                }
                
            }
            else
            {
                multipler += ris.Days*20;
                if (multipler > 600) multipler = 60;
            }

            //for(int i = 0; i < current.Length; i++)
            //{
            //    app[i] = Math.Abs(current[i] - next[i]);
            //}

            

            //if (app[4] != 0) multipler += app[4] * 1;
            //else if (app[3] != 0) multipler += app[3] * 0.5;
            //else if (app[2] != 0) multipler += app[2] * 0.25;
            //else if (app[1] != 0) multipler += app[1] * 0.025;
            //else if (app[0] != 0) multipler += app[0] * 0.0025;
            return multipler;
        }

        private DateTime TrimDate(int posizione)     //metodo 100% appoggio
        {
            string[] strSplit = cronologia[posizione].Split();
            string[] anni = strSplit[0].Split('-');
            string[] minuti = strSplit[1].Split(':');

            DateTime current = new DateTime(int.Parse(anni[0]), int.Parse(anni[1]), int.Parse(anni[2]),
                                    int.Parse(minuti[0]), int.Parse(minuti[1]), int.Parse(minuti[2]));
            return current;
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
            Fusione();
            LenghtLine(0);
        }
    }
}