using Lottie.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Login.PostLogin.AllSnack.Page;
using System.Collections;
using System.Timers;

namespace fondomerende.Main.Login.PostLogin.AllSnacks.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnackViewCell : ViewCell
    {
        bool AlmostDone;
        AnimationView pacMananimation;
        private static Timer aTimer;

        public SnackViewCell()
        {
            InitializeComponent();


            pacMananimation = new AnimationView
            {
                Animation = "pacman0.6.json",
                Scale = 1.5,
                Loop = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutoPlay = true,
                HardwareAcceleration = true,
                InputTransparent = true,
                IsVisible = false,
                Speed = 4,
            };

            Grid.Children.Add(pacMananimation, 0, 0);
            MessagingCenter.Subscribe<SnackViewCell>(this, "Animate", async (arg) =>
            {
                if (Nome.Text == AllSnacksPage.selectedItemBinding)
                {
                    pacManAnimate();
                }
            });
        }

        private async void pacManAnimate()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            pacMananimation.IsVisible = true;
            pacMananimation.Margin = new Thickness(-mainDisplayInfo.Width / 0.8, 0, 0, 0);
            await Task.WhenAny<bool>
            (
             pacMananimation.TranslateTo((mainDisplayInfo.Width / 0.8)-((mainDisplayInfo.Width/0.8) / 6), 0, Convert.ToUInt32(((mainDisplayInfo.Width / 0.8) / 0.096)))
            );
            QtaRefresh();

        }
        async void QtaRefresh()
        {
          qta.Text = Convert.ToString(Int32.Parse(qta.Text) - 1);
        }
    }
}
