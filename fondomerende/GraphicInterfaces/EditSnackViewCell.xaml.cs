using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.GraphicInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSnackViewCell : ViewCell
    {
        public EditSnackViewCell()
        {
            InitializeComponent();
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {

        }
    }
}