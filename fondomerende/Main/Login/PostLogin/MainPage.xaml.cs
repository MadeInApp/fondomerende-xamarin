﻿using fondomerende.Main.Manager;
using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using fondomerende.Main.Utilities;

namespace fondomerende.Main.Login.PostLogin
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

            SnacksNavPage.IconImageSource = ImageSource.FromResource("fondomerende.image.vm_icon_64x64.png");
            SettingsNavPage.IconImageSource = ImageSource.FromResource("fondomerende.image.settings_icon_64x64.png");
        }
    }
}

