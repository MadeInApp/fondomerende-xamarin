using fondomerende.GraphicInterfaces;
using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.PostLoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSnackSettingsPage : ContentPage
    {
        public EditSnackSettingsPage()
        {
            InitializeComponent();
            GetSnackDataMethod();
        }

        public async void GetSnackDataMethod()
        {
            SnackServiceManager snackService = new SnackServiceManager();
            snackService.GetSnackAsync(EditSnackListPage.SelectedSnack);
            Nome.Text = EditSnackListPage.SelectedSnack;
        }

        private void ApplyChanges_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}