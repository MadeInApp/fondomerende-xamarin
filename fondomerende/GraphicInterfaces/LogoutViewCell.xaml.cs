using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.GraphicInterfaces;

namespace fondomerende.GraphicInterfaces
{

    [DesignTimeVisible(true)]
    public partial class LogoutViewCell : ViewCell
    {
        public LogoutViewCell()
        {
            InitializeComponent();
        }

        public async void LogoutCellTapped(object sender, EventArgs e)
        {
            
            var ans = await App.Current.MainPage.DisplayAlert("Fondo Merende", "Vuoi davvero effettuare il Log Out?", "Si", "No");
            if (ans)
            {
                LogoutServiceManager logoutService = new LogoutServiceManager();
                var response = await logoutService.LogoutAsync();
                if (response.response.success == true)
                {
                    App.Current.MainPage = new LoginPage();
                    Preferences.Clear();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
                }
            }
        }





        public void SetImageColor(Color c)
        {
            //LogoutButton. = c ;
        }
    }
}