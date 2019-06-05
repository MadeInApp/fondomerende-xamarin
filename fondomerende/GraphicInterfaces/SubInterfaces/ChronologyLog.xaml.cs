using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.GraphicInterfaces.SubInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChronologyLog : ContentPage
    {
        public ChronologyLog()
        {
            InitializeComponent();
            cose();
        }

        public async void cose()
        {
            LastActionServiceManager lastAction = new LastActionServiceManager();
            var result = await lastAction.GetLastActions();

            if (result.response.success == true)
            {
                var a = result.data;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
            }
        }
    }
}