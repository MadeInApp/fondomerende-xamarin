using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Services.RESTServices;
using fondomerende.Manager;

namespace fondomerende.PostLoginPages.GraphicInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserViewCell : ViewCell
    {
        public string firstLetterIcon;
    
        public EditUserViewCell()
        {
            InitializeComponent();
            First_letter();
            GetUserFundsMethod();
            friendly_name.Text = InformationFriendlyName();
            Cerchio.BackgroundColor = Color.FromHex(Preferences.Get("Colore", "#CCCCCC"));

        }


        public void First_letter()        //Grafica
        {
            string firstLetter = "";
            Preferences.Get("friendly-name", "");

            string[] strSplit = Preferences.Get("friendly-name", "").Split();

            foreach (string res in strSplit)
            {
                firstLetter = (res.Substring(0, 1));
            }
            inizialeLabel.Text = firstLetter;
        }

        public string InformationFriendlyName() => Preferences.Get("friendly-name", "");

        public void Bottone_ClickedAsync(object sender, EventArgs e)  // modifica i vari colori 
        {
            ColorRandom c = new ColorRandom();
            Color color = c.GetRandomColor();
            Cerchio.BackgroundColor = color;

            //momentaneo poi lo faremo meglio
            LogoutViewCell lo = new LogoutViewCell();
            ChronologyViewCell ch = new ChronologyViewCell();
            EditSnackViewCell ed = new EditSnackViewCell();

            lo.SetImageColorPreferences();
            ch.SetImageColorPreferences();
            ed.SetImageColorPreferences();

        }

        public async Task<string> GetUserFundsMethod()
        {
            UserFundsServiceManager userFundsService = new UserFundsServiceManager();
            var result =  await userFundsService.GetUserFunds();
            if(result.response.success == true)
            {
                userFunds.Text = "€" + result.data.user_funds_amount;
                if (float.Parse(result.data.user_funds_amount) <= 0)
                {
                    userFunds.TextColor = Color.Red;
                }
            }
            else
            {
                userFunds.Text = "Errore";
            }
            return result.response.message;
        }
    }
}

