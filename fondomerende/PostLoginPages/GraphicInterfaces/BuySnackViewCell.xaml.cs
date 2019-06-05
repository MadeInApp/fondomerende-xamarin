﻿using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.PostLoginPages.GraphicInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackViewCell : ViewCell
    {
        public BuySnackViewCell()
        {
            InitializeComponent();
            SetImageColorPreferences();
        }

        public void SetImageColorPreferences()
        {
            BuyIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }
    }
}