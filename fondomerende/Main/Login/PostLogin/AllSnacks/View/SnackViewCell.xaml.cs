﻿using Lottie.Forms;
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
                Scale = 1.6,
                Loop = true,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutoPlay = true,
                HardwareAcceleration = true,
                InputTransparent = true,
                IsVisible = false,
                Speed = 4,
            };

            Grid.Children.Add(pacMananimation);
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
            double pacmanwidth = 120;
            var mainDisplayInfo = App.Current.MainPage.Width;//DeviceDisplay.MainDisplayInfo.Width;
            pacMananimation.IsVisible = true;
            // pacMananimation.Margin = new Thickness(-mainDisplayInfo.Width / 0.8, 0, 0, 0);
            //await Task.WhenAny<bool>
            //(
            // pacMananimation.TranslateTo((mainDisplayInfo.Width / 0.8)-((mainDisplayInfo.Width/0.8) / 4.40), 0, Convert.ToUInt32(((mainDisplayInfo.Width / 0.8) / 0.096)))
            //);
            await pacMananimation.TranslateTo(Convert.ToInt32(-mainDisplayInfo + pacmanwidth), 0, 0);
            await Task.WhenAny<bool>
            (
             pacMananimation.TranslateTo(Convert.ToInt32(mainDisplayInfo - pacmanwidth*2) , 0, 7000)
            );
            QtaRefresh();
            //await pacMananimation.TranslateTo((mainDisplayInfo / 0.8), 0, Convert.ToUInt32(((mainDisplayInfo / 0.8) / 0.40)));
             await pacMananimation.TranslateTo(Convert.ToInt32(mainDisplayInfo), 0, 4000);
        }
        async void QtaRefresh()
        {
          qta.Text = Convert.ToString(Int32.Parse(qta.Text) - 1);
        }
    }
}
