using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.PostLoginPages.GraphicInterfaces;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.ViewCell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSnackViewCell : ViewCell
    {
        public AddSnackViewCell()
        {
            InitializeComponent();
            SetImageColorPreferences();
        }

        public void SetImageColorPreferences()
        {
            SnackIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }
    }
}