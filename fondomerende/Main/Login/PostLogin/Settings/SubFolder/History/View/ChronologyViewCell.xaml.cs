using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.Content;
using FormsControls.Base;
using fondomerende.Main.Utilities;
using Plugin.CrossPlatformTintedImage.Abstractions;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.View
{

    [DesignTimeVisible(true)]
    public partial class ChronologyViewCell : ViewCell
    {
        public ChronologyViewCell()
        {
            InitializeComponent();
            Tint();

        }
        public void Tint()
        {
            ChronologyIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }

        public void InizializeImage()
        {
            EmbeddedImage e = new EmbeddedImage();
            e.Resource = "fondomerende.image.History.png";

            var tinted = new TintedImage
            {
                Scale = 0.5,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Aspect = Aspect.AspectFill,
                Source = e.Resource,
            };

            tinted.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
            Griglia.Children.Add(tinted,0,0);
        }
    }
}