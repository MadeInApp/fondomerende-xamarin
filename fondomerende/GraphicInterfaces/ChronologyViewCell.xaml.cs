using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.GraphicInterfaces;
using Android.Content.Res;
using fondomerende.GraphicInterfaces.SubInterfaces;
using FormsControls.Base;
using fondomerende.PostLoginPages;

namespace fondomerende.GraphicInterfaces
{

    [DesignTimeVisible(true)]
    public partial class ChronologyViewCell : ViewCell
    {
        public ChronologyViewCell()
        {
            InitializeComponent();
            SetImageColorPreferences();
 
        }

        public async void ChronologyCellTapped(object sender, EventArgs e)
        {
            //var a = new InterfaceImplementedPage();
            AnimationNavigationPage a= new AnimationNavigationPage();
            IPageAnimation PageAnimation  = new SlidePageAnimation { Duration = AnimationDuration.Long, Subtype = AnimationSubtype.FromTop };

            App.Current.MainPage = new AnimationNavigationPage(new ChronologyLog());

        }


        public void SetImageColorPreferences()
        {
            ChronologyIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }


     
    }
}