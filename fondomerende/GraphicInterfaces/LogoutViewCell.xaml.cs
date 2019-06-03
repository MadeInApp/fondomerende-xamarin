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
            LogoutServiceManager logoutService = new LogoutServiceManager();
            var response = await logoutService.LogoutAsync();


            if (response.response.success == true)
            {
                App.Current.MainPage = new LoginPage();
            }
            else
            {
                await DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
            }

        }

        private Task DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }
    }
}