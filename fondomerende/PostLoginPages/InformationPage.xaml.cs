﻿using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using Xamarin.Essentials;
using fondomerende.Services.Models;

namespace fondomerende.PostLoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationPage : ContentPage
    {
        public object LoggedAs { get; }
        public string firstLetterIcon = "dd";
        public string friendly_name = "111";

        public InformationPage()
        {
            InitializeComponent();

            listView.ItemsSource = new List<string> { "" };
            
            firstLetterIcon = First_letter();
            friendly_name = InformationFriendlyName();
            /*LoggedAs.Text = "Loggato come: " + Preferences.Get("username", null);   //semplice testo che ti dice il nome dell'account con cui sei loggato
            LoggedAs.Opacity = 0.5;*/
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
                App.Current.MainPage = new LoginPage();
                Preferences.Clear();
            }
            else
            {
                await DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
            }
        }

        public string First_letter()        //Grafica
        {
            string firstLetter="";
            Preferences.Get("friendly-name", "");

            string[] strSplit = Preferences.Get("friendly-name", "").Split();
                
            foreach (string res in strSplit)
            {
                firstLetter = (res.Substring(0, 1));
            }
            return firstLetter;
        }

        public string InformationFriendlyName() => Preferences.Get("friendly-name", "");
    }
}