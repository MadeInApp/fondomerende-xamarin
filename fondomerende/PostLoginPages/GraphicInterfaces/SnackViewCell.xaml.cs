using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.PostLoginPages.GraphicInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnackViewCell : ViewCell
    {
        public SnackViewCell()
        {
            InitializeComponent();
           // GoogleImage_View.Source = ImageSource.FromResource("fondomerende.image.CheckBox_empty_32x32.png");

        }
    }
}