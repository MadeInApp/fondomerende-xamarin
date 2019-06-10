using fondomerende.PostLoginPages.GraphicInterfaces;
using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page
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
            var result = await snackService.GetSnackAsync(EditSnackListPage.SelectedSnackName);
            Nome.Text = EditSnackListPage.SelectedSnackFriendlyName;
           // Prezzo.Text = Convert.ToString(result.data.snack.price);
          /*  SnackPerScatola.Text = Convert.ToString(result.data.snack.snack_per_box);
            Scadenza.Text = Convert.ToString(result.data.snack.expiration_in_days);
*/
        }

        private void ApplyChanges_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}