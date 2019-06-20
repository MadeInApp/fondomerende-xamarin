using ColorPicker;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.Popup;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Settaggio.PopUp;
using FormsControls.Base;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.About_and_UserSettings.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : AnimationPage
    {
        private readonly ColorPickerPopup _colorPickerPopup;
        public AboutPage()
        {
            InitializeComponent();
            _colorPickerPopup = new ColorPickerPopup();
            _colorPickerPopup.ColorChanged += ColorPickerPopupOnColorChanged;
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

        private void ColorPickerPopupOnColorChanged(object sender, ColorChangedEventArgs args)
        {
            Preferences.Set("Colore", GetHexString(args.Color));
        }
        public static string GetHexString(Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
        private void EditUserInfoViewCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new EditUserInfoPopUp());
        }

        private void ChangeColorViewCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new ColorPickerPopup());
        }
    }
}