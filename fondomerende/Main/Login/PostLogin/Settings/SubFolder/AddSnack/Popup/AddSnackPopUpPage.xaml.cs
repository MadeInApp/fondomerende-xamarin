using fondomerende.Main.Services.RESTServices;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fondomerende.Main.Login.LoginPages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.Page;
using fondomerende.Main.Utilities;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSnackPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        
        SnackServiceManager snackService = new SnackServiceManager();
        LineEntry NomeSnack;
        LineEntry PrezzoSnack;
        LineEntry SnackPerBox;
        LineEntry ExpInDays;
        LineEntry Qta;
        public AddSnackPopUpPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            AddSnackPage.clicked = true;
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }



        //per applicare le modifiche//
        private async void Apply_Clicked(object sender, EventArgs e)
        {
            SnackServiceManager snackService = new SnackServiceManager();

            if (Nome.Text == null)
            {
                ErrorLabel.Text = "Inserire un nome";
            }
     
            if(Prezzo.Text == null)
            {
                ErrorLabel.Text = "Inserire un prezzo";
            }
            
            if(SnackPerScatola.Text == null)
            {
                ErrorLabel.Text = "Inserire uno snack";
            }
           
            if (ScadenzaInGiorni.Text == null)
            {
                ErrorLabel.Text = "Immettere un giorno di scadenza";
            }

            else
            {
                var ans = await DisplayAlert("Fondo Merende", "Lo Snack è contabile?", "Si", "No");
                if (ans)
                {
                    var result = await snackService.AddSnackAsync(Nome.Text, double.Parse(Prezzo.Text), int.Parse(SnackPerScatola.Text), int.Parse(ScadenzaInGiorni.Text), true);
                    if (result.response.success)
                    {

                        await DisplayAlert("Fondo Merende", "SnackID: " + result.response.data.id, "Ok");
                        Navigation.PopPopupAsync();
                    }
                    else
                    {
                        await DisplayAlert("Fondo Merende", "Snack già presente", "Ok");
                    }
                }
                else
                {
                    var result = await snackService.AddSnackAsync(Nome.Text, double.Parse(Prezzo.Text), int.Parse(SnackPerScatola.Text), int.Parse(ScadenzaInGiorni.Text), false);
                    if (result.response.success)
                    {

                        await DisplayAlert("Fondo Merende", "SnackID: " + result.response.data.id, "Ok");
                        Navigation.PopPopupAsync();
                    }
                    else
                    {
                        await DisplayAlert("Fondo Merende", "Snack già presente", "Ok");
                    }
                }
            }
            
        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
