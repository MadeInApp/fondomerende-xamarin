using fondomerende.Services.RESTServices;
using FormsControls.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.PostLoginPages.GraphicInterfaces.SubInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChronologyLogView : ViewCell
    {
        public ChronologyLogView()
        {
            InitializeComponent();
            switch (Device.RuntimePlatform)             //Se il dispositivo è Android non mostra la Top Bar della Navigation Page, se è iOS la mostra
            {
                default:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;

                   

            }
            GetLastAction();
            //MyPageAnimation = new SlidePageAnimation()
            //{
            //    Duration = AnimationDuration.Long,
            //    Subtype = AnimationSubtype.FromLeft
            //};
        }

        public async void GetLastAction() //roba non funzionante
        {
            LastActionServiceManager lastAction = new LastActionServiceManager();
            var result = await lastAction.GetLastActions();

            if (result.response.success == true)
            {
                string[] cronologia = result.data.actions;
                ChronologyLabel.Text = cronologia[0];
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
            }
        }

        public IPageAnimation MyPageAnimation { get; set; }
        
    }
}