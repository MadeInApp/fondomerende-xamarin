using fondomerende.Main.Services.RESTServices;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using fondomerende.Main.Services.Models;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser;
using fondomerende.Main.Login.LoginPages;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Page;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        EditUserServiceManager editUser = new EditUserServiceManager();
        SnackServiceManager snackService = new SnackServiceManager();
        private static bool click = false;

        

        public EditUserPopUpPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            EditUserPopUpPage.click = true;
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


        private async void Apply_Clicked(object sender, EventArgs e)
        {
            
            string oldPAssword = Preferences.Get("password", null);
            if(oldPAssword.Equals(newPassword.Text))
            {
                var risp = await editUser.EditUserAsync(EditUserPage.username, EditUserPage.FriendlyName, EditUserPage.passwordNuova);
                if (risp.response.success == true)
                {

                    await DisplayAlert("Fondo Merende", "Impostazioni Cambiate", "Ok");
                    Xamarin.Essentials.Preferences.Clear();
                    Application.Current.MainPage = new LoginPage();

                }
                else
                {
                    await DisplayAlert("Fondo Merende", "Password errata", "Ok");
                }
            }
            
            

        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }



    }
}