using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using fondomerende.PostLoginPages.GraphicInterfaces.SubInterfaces;
using FormsControls.Base;

namespace fondomerende.PostLoginPages.GraphicInterfaces
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