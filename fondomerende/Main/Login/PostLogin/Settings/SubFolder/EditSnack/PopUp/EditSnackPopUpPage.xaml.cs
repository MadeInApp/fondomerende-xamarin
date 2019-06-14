using fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.Page;
using fondomerende.Main.Services.RESTServices;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fondomerende.Main.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditSnack.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSnackPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        EmbeddedImage img = new EmbeddedImage();
        SnackServiceManager snackService = new SnackServiceManager();
        int snackID = EditSnackListPage.SelectedSnackID;
        bool Swapped = true;
        int Quantity;
        public EditSnackPopUpPage()
        {
            InitializeComponent();
            NomeSnack.Placeholder = EditSnackListPage.SelectedSnackName;
            PrezzoSnack.Placeholder = Convert.ToString(EditSnackListPage.SelectedSnackPrice);
            SnackPerBox.Placeholder = Convert.ToString(EditSnackListPage.SelectedSnackPerBox);
            ExpInDays.Placeholder = Convert.ToString(EditSnackListPage.SelectedSnackExpiration);
            GetQta();
            
        }
        public async void GetQta()
        {
            Qta.Placeholder = null;
            Quantity = 0;
            var result = await snackService.GetSnacksAsync();
            if (result.response.success)
            {
                for (int i = 0; i <= result.data.snacks.Count; i++)
                {
                    if(result.data.snacks[i].id == EditSnackListPage.SelectedSnackID)
                    {
                        Qta.Placeholder = Convert.ToString(result.data.snacks[i].quantity);
                        Quantity = result.data.snacks[i].quantity;
                        return;
                    }
                }
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }


        private async void Apply_Clicked(object sender, EventArgs e)
        {
            if (NomeSnack.Text == null || PrezzoSnack.Text == null || SnackPerBox.Text == null || ExpInDays.Text == null || Qta.Text == null)
            {
                await DisplayAlert("Fondo Merende", "Riempi tutti i campi", "Ok");
            }
            else if(Int32.Parse(PrezzoSnack.Text) == 0 || Int32.Parse(SnackPerBox.Text) == 0)
            {
                await DisplayAlert("Fondo Merende", "Il Prezzo e 'gli Snacks per scatola' devono essere maggiori di 0", "Ok");
            }
            else
            {
                var res = await snackService.EditSnackAsync(snackID, NomeSnack.Text, PrezzoSnack.Text, SnackPerBox.Text, ExpInDays.Text, Int32.Parse(Qta.Text));
                if (res.response.success)
                {
                    await Navigation.PopPopupAsync();
                }
                else
                {
                    await DisplayAlert("Fondo Merende", "Errore", "Ok");
                }
            }
        }

        private async void Discard_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        private async void Swap_Clicked(object sender, EventArgs e)
        {
            if (Swapped == true)
            {
                Swap.Source = ImageSource.FromResource("fondomerende.image.fill_clear_256x256.png");
                NomeSnack.Placeholder = null;
                PrezzoSnack.Placeholder = null;
                SnackPerBox.Placeholder = null;
                ExpInDays.Placeholder = null;
                if(NomeSnack.Text == null)
                {
                    NomeSnack.Text = EditSnackListPage.SelectedSnackName;
                }
                if(PrezzoSnack.Text == null)
                {
                    PrezzoSnack.Text = Convert.ToString(EditSnackListPage.SelectedSnackPrice);
                }
                if(SnackPerBox.Text == null)
                {
                    SnackPerBox.Text = Convert.ToString(EditSnackListPage.SelectedSnackPerBox);
                }
                if(ExpInDays.Text == null)
                {
                  ExpInDays.Text = Convert.ToString(EditSnackListPage.SelectedSnackExpiration);
                }
                if(Qta.Text == null)
                {
                    Qta.Text = Convert.ToString(Quantity); 
                }
                Swapped = false;
            }
            else
            {
                Swap.Source = ImageSource.FromResource("fondomerende.image.fill_full_256x256.png");
                NomeSnack.Text = null;
                PrezzoSnack.Text = null;
                SnackPerBox.Text = null;
                ExpInDays.Text = null;
                Qta.Text = null;
                NomeSnack.Placeholder = EditSnackListPage.SelectedSnackName;
                PrezzoSnack.Placeholder = Convert.ToString(EditSnackListPage.SelectedSnackPrice);
                SnackPerBox.Placeholder = Convert.ToString(EditSnackListPage.SelectedSnackPerBox);
                ExpInDays.Placeholder = Convert.ToString(EditSnackListPage.SelectedSnackExpiration);
                Qta.Placeholder = Convert.ToString(Quantity);
                Swapped = true;
            }
        }


    }
}