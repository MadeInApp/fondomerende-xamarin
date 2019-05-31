﻿using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace fondomerende.PostLoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationPage : ContentPage
    {

        public InformationPage()
        {
        InitializeComponent();
            LoggedAs.Text = "Loggato come: " + Preferences.Get("username", null);   //semplice testo che ti dice il nome dell'account con cui sei loggato
            LoggedAs.Opacity = 0.5;
            switch (Device.RuntimePlatform)             //Se il dispositivo è Android non mostra la Top Bar della Navigation Page, se è iOS la mostra
            {
                default:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;

            }


        }

        private bool DevicePlatform(bool v1, bool v2, bool v3)
        {
            throw new NotImplementedException();
        }

        private async void LogOut_button_Clicked(object sender, EventArgs e)        //effettua il Log Out
        {
            LogoutServiceManager logoutService = new LogoutServiceManager();
            var response = await logoutService.LogoutAsync();


            if (response.response.success == true)
            {
                await Navigation.PopAsync();

                Preferences.Clear();
            }
            else
            {
                await DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
            }
        }
    }
}