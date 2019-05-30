using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace fondomerende.PostLoginPages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : TabbedPage
    {

        public string GetSnackName;
        public MainPage()
        {
            switch (Device.RuntimePlatform)
            {
                default:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;
            }

            InitializeComponent();
            SnacksNavPage.IconImageSource = ImageSource.FromResource("fondomerende.image.vm_icon_64x64.png");
            SettingsNavPage.IconImageSource = ImageSource.FromResource("fondomerende.image.settings_icon_64x64.png");
        }


        }
    }

