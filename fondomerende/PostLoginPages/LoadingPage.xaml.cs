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
    }
}