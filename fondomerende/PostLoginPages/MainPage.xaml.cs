using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;



namespace fondomerende
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : TabbedPage
    {

        public string GetSnackName;
        public MainPage()
        {
            InitializeComponent();
            NavPage.IconImageSource = ImageSource.FromResource("fondomerende.image.vm_icon_51x51.png");
            
           // vending_machine_logo.source = ImageSource.FromResource("fondomerende.image.vending-machine_icon.png");
        }

      
    }
}
