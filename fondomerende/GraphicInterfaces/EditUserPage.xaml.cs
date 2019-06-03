﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Services.RESTServices;

namespace fondomerende.GraphicInterfaces
{
    public partial class EditUserPage : ContentPage
    {
        
        public EditUserPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void ApplyChanges_Clicked_1(object sender, EventArgs e)
        {
            string username = Preferences.Get("username", null);
            string FriendlyName = Preferences.Get("friendly-name", null);
            string password = Preferences.Get("password", null);

            EditUserServiceManager editUser = new EditUserServiceManager();



            if (usernameEntry.Text != null)
            {
                username = usernameEntry.Text;
            }
            if (friendlynameEntry.Text != null)
            {
                FriendlyName = friendlynameEntry.Text;
            }
            if (passwordEntry.Text != null)
            {
                password = passwordEntry.Text;
            }


            if (OldpasswordEntry.Text == Preferences.Get("password", null))
            {
                var result = await editUser.EditUserAsync(FriendlyName, username, password);
                  if (result.response.success == true)
                    {
                        await DisplayAlert("Fondo Merende", "Impostazioni Cambiate", "Ok");
                        Preferences.Clear();
                        App.Current.MainPage = new LoginPage();
                  }
            }
            else
            {
                await DisplayAlert("Fondo Merende", "Password Errata", "Ok");
            }

            
        }
        private async void CancelButton_Clicked(object sender, EventArgs e) => await Navigation.PopAsync();
        
    }
}