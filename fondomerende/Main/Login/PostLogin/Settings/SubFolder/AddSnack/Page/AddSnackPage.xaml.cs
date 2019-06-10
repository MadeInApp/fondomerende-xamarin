using fondomerende.Services.Models;
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
    public partial class AddSnackPage : ContentPage
    {
        SnackServiceManager snackService = new SnackServiceManager();
        bool clicked = false;
        public AddSnackPage()
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
        }


        private async void ApplyChanges_Clicked(object sender, EventArgs e)
        {
            var ans = await DisplayAlert("Fondo Merende", "Lo Snack è contabile?", "Si", "No");
            if(ans)
            {
              var result = await snackService.AddSnackAsync(Nome.Text, Double.Parse(Prezzo.Text), Int32.Parse(SnackPerScatola.Text), Int32.Parse(ScadenzaInGiorni.Text), true);
                if(result.response.success)
                {
                    await DisplayAlert("Fondo Merende", "SnackID: " + result.response.data.id, "Ok");
                }
                else
                {
                    await DisplayAlert("Fondo Merende", "Errore", "Ok");
                }
            }
            else
            {
                await snackService.AddSnackAsync(Nome.Text, Double.Parse(Prezzo.Text), Int32.Parse(SnackPerScatola.Text), Int32.Parse(ScadenzaInGiorni.Text), false);
            }
        }
    }
}