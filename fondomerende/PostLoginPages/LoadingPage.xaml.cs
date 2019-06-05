using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.PostLoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
            LabelRandom c = new LabelRandom();
            String rPhrase = c.GetRandomPhrases();
            LoadingLabel.Text = rPhrase;


            LogIn();
            Ciambella();
            




        }

        public async void LogIn()
        {
            LoginServiceManager login = new LoginServiceManager();
            var resultLogin = await login.LoginAsync(Preferences.Get("username", ""), Preferences.Get("password", ""), true);
            if (!resultLogin.response.success)
            {
                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                App.Current.MainPage = new MainPage();
            }
        }

        private async void Ciambella()
        {
            await Fondo_Merende_logo.ScaleTo(0.2, 0);
            await Fondo_Merende_logo.TranslateTo(0, 0, 500);

            await Task.WhenAny<bool>
                (
                    Fondo_Merende_logo.ScaleTo(0.8, 500),
                    Fondo_Merende_logo.RotateTo(360, 500)
                );
            
            await Fondo_Merende_logo.ScaleTo(1.2, 150);
            await Fondo_Merende_logo.ScaleTo(1.0, 250);
            await Fondo_Merende_logo.TranslateTo(0, 0, 1500);


            //await Task.WhenAny<bool>
            //   (
            //    Fondo_Merende_logo.FadeTo(1, 0),
            //    Fondo_Merende_logo.ScaleTo(0.2, 0)
            //   );

            //await Task.WhenAny<bool>
            //    (
            //    Fondo_Merende_logo.FadeTo(1, 200),
            //    Fondo_Merende_logo.ScaleTo(0.9, 800),
            //    Fondo_Merende_logo.RotateTo(360, 800)
            //   );

            //await Fondo_Merende_logo.ScaleTo(1.2, 200);
            //await Fondo_Merende_logo.ScaleTo(1.0, 200);
        }
    }
}