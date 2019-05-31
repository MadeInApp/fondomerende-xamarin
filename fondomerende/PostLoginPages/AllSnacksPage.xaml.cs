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
            switch (Device.RuntimePlatform)
            {
                default:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;

            }



            ListView.RefreshCommand = new Command(async () => 
        {
            await RefreshDataAsync();
            ListView.IsRefreshing = false;
        });


        }
        public async Task RefreshDataAsync()
        {
           await GetSnacksMethod();
        }



     public async Task GetSnacksMethod()
        {
           
            var result = await snackServiceManager.GetSnacksAsync();
            AllSnacks = result.data.snacks;
            ListView.ItemsSource = AllSnacks;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var ans =  await DisplayAlert("Fondo Merende", "Vuoi davvero mangiare " + (e.SelectedItem as SnackDataDTO).friendly_name + "?", "Si", "No");

            if (ans == true)
            {
              await snackServiceManager.EatAsync((e.SelectedItem as SnackDataDTO).id, 1);
              await DisplayAlert("Fondo Merende", (e.SelectedItem as SnackDataDTO).friendly_name + " mangiato/i", "ok");
              await  GetSnacksMethod();

            } 
            else
            {
                GetSnacksMethod();
            }
        }
    }
}
