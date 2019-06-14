using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Popup;
using Foundation;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.About_and_UserSettings.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            Version.Text = "Version:" + "  " + NSBundle.MainBundle.InfoDictionary["CFBundleVersion"];
        }

        private void EditUserInfoViewCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new EditUserInfoPopUp());
        }
    }
}