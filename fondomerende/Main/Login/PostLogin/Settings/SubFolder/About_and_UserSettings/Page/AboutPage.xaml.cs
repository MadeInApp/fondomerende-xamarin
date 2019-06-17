using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Popup;
using FormsControls.Base;
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
    public partial class AboutPage : AnimationPage
    {
        public AboutPage()
        {
            InitializeComponent();
            Version.Text = "Version:" + "0.5";
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

        private void EditUserInfoViewCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new EditUserInfoPopUp());
        }
    }
}