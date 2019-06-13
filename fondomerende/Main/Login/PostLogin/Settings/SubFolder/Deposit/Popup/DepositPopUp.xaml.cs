﻿using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page;
using fondomerende.Main.Services.RESTServices;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DepositPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        public DepositPopUp()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            BuySnackListPage.Refresh = true;
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
            DepositServiceManager depositService = new DepositServiceManager();
            if(Amount.Text == null)
            {
                ErrorLabel.Text = "Inserisci l'ammontare";
            }
            string[] strSplit = Amount.Text.Split(',');
            float ris = float.Parse(strSplit[0]) + (float.Parse(strSplit[1]) / 100);
            if(ris > 0)
            {
                var resultDep = await depositService.DepositAsync(Convert.ToDecimal(ris));
                if (resultDep.response.success)
                {
                    Navigation.PopPopupAsync();
                }
                else
                {
                    await DisplayAlert("Fondo Merende", "Errore", "Ok");
                }
                if(resultDep == null)
                {
                    ErrorLabel.Text = "Errore";
                }
            }
            else
            {
                await DisplayAlert("Fondo Merende","L'ammontare non può essere minore di 1", "Ok");
            }
        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private void Amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int app = Convert.ToInt32(Amount.Text);
            //if(app < 100 && app >= 10)
            //{
            //    Amount.Text = Amount.Text + ",";
            //}
            
        }
    }
}