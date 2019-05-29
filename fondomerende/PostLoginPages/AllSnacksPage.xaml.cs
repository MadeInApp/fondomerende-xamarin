using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Services.Models;

namespace fondomerende
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllSnacksPage : ContentPage
    {
        List<SnackDataDTO> AllSnacks = new List<SnackDataDTO>();

        public AllSnacksPage()
        {
            InitializeComponent();
            GetSnacksMethod();


        }
        public async void GetSnacksMethod()
        {
            SnackServiceManager snackServiceManager = new SnackServiceManager();
            var result = await snackServiceManager.GetSnacksAsync();
            AllSnacks = result.data.snacks;
            ListView.ItemsSource = AllSnacks;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new SnackOptionsPage());
        }
    }
}
