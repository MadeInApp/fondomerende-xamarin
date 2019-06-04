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
            LastActionServiceManager lastAction = new LastActionServiceManager();
            var result = await lastAction.GetLastActions();

            if(result.response.success == true)
            {

            }
        }


        public void SetImageColorPreferences()
        {
            ChronologyIcon.TintColor = Color.FromHex(Preferences.Get("Colore", "#000000"));
        }


     
    }
}