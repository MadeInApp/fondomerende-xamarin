using Lottie.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.AllSnacks.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnackViewCell : ViewCell
    {
        AnimationView pacMananimation;
        public SnackViewCell()
        {
            InitializeComponent();


            pacMananimation = new AnimationView
            {
                Animation = "pacman0.6.json",
                Loop = true,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutoPlay = true,
                InputTransparent = true,
                IsVisible = true,
                Margin = new Thickness(0,0,0,0)
            };

            PacManTraslate();

        }

        async void PacManTraslate()
        {
           Stack.Children.Add(pacMananimation);
           await Task.WhenAny<bool>
            (
             pacMananimation.TranslateTo(150,0, 10000)
            );
        }
    }
}