using fondomerende.Main.Login.PostLogin.Settings.SubFolder.AddSnack.Popup;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.Deposit.Popup;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.Content;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Deposit.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DepositPage : ContentPage
    {
        public DepositPage()
        {
            InitializeComponent();
            gesture();
        }

        private void gesture()
        {
            TapGestureRecognizer tg1 = new TapGestureRecognizer();
            tg1.Tapped += DepositaCliccato;
            Deposita.GestureRecognizers.Add(tg1);
        }

        private async void DepositaCliccato(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new DepositPopUp());
        }

        private async void CronologiaCliccato(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChronologyContentPage());
        }

        private async void AddSnackCliccato(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new AddSnackPopUpPage());
        }

        private async void BuySnackCliccato(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuySnackListPage());
        }

        private async void EditSnackCliccato(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditSnackListPage());
        }
    }
}