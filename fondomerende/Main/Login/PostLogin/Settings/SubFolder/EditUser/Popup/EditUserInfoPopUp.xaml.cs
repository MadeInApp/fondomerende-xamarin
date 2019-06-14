using fondomerende.Main.Services.RESTServices;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserInfoPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static string username = "";
        public static string FriendlyName = "";
        public static string passwordNuova = "";
        public string GetpasswordNuova()
        {
            return passwordNuova;
        }

        private void SetpasswordNuova(string value)
        {
            passwordNuova = value;
        }

        public EditUserInfoPopUp()
        {
            InitializeComponent();
            usernameEntry.Placeholder = Preferences.Get("username", null);
            friendlynameEntry.Placeholder = Preferences.Get("friendly-name", null);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
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


        private async void ApplyChanges_Clicked_1(object sender, EventArgs e)
        {
            EditUserServiceManager editUser = new EditUserServiceManager();
            if (usernameEntry.Text != null && friendlynameEntry.Text != null && passwordEntry.Text != null)
            {

                username = usernameEntry.Text;
                FriendlyName = friendlynameEntry.Text;
                passwordNuova = passwordEntry.Text;
                Navigation.PushPopupAsync(new EditUserPopUpPage());
            }
            else
            {
                await DisplayAlert("Fondo Merende", "inserisci tutti i dati", "Ok");
            }
        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}