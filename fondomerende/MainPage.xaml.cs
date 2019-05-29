using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace fondomerende
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {

        public string GetSnackName;
        public MainPage()
        {
            InitializeComponent();
        }

        private void GetSnackData_Clicked(object sender, EventArgs e)
        {
         
        }

        private void GetAllSnacksData_Clicked(object sender, EventArgs e)
        {
      
        }

        private async void ButtonSearch_Clicked(object sender, EventArgs e)
        {
            if (EntryGetSnack.Text != null)
            {
                GetSnackName = EntryGetSnack.Text;
                SnackServiceManager snackService = new SnackServiceManager();
                var SnackIstance = await snackService.GetSnackAsync(GetSnackName);
                if (SnackIstance.response.success == true)
                {
                    await DisplayAlert("FondoMerende", SnackIstance.data.ToString(), "OK");
                }

            }
        }
    }
}
