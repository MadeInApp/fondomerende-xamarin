using fondomerende.Services.RESTServices;
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
    public partial class BuySnackListViewCell : ViewCell
    {
        SnackServiceManager snackService = new SnackServiceManager();
        public BuySnackListViewCell()
        {
            InitializeComponent();

        }

    }
}