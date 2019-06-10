using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.Content;
using FormsControls.Base;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View
{

    [DesignTimeVisible(true)]
    public partial class ChronologyViewCell : ViewCell
    {
        public ChronologyViewCell()
        {
            InitializeComponent();
            SetImageColorPreferences();
 
        }


        public void SetImageColorPreferences()
        {
            ChronologyIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }


     
    }
}