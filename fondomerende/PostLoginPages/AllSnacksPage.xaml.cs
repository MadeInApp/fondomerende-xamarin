using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using fondomerende.Services.Models;

namespace fondomerende.PostLoginPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllSnacksPage : ContentPage
    {
        SnackServiceManager snackServiceManager = new SnackServiceManager();
        List<SnackDataDTO> AllSnacks = new List<SnackDataDTO>();

        public AllSnacksPage()
        {
            InitializeComponent();
            GetSnacksMethod();


        }

        public async void GetSnacksMethod()
        {
           
            var result = await snackServiceManager.GetSnacksAsync();
            AllSnacks = result.data.snacks;
            ListView.ItemsSource = AllSnacks;
        }

        private async void ListView_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            await snackServiceManager.EatAsync((e.SelectedItem as SnackDataDTO).id, 1);
            await DisplayAlert("Fondo Merende", (e.SelectedItem as SnackDataDTO).name + " mangiato/i", "ok");
        }
    }
}
