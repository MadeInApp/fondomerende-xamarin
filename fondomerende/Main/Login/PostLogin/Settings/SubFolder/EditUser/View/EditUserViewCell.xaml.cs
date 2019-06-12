using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Services.RESTServices;
using fondomerende.Main.Manager;
using fondomerende.Main.Utilities;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.EditUser.View
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

            string[] strSplit = Preferences.Get("friendly-name", "").Split();

            foreach (string res in strSplit)
            {
                firstLetter = (res.Substring(0, 1));
            }
            inizialeLabel.Text = firstLetter;
            inizialeLabel_iOS.Text = firstLetter;
        }

        public string InformationFriendlyName() => Preferences.Get("friendly-name", "");

        public void Bottone_ClickedAsync(object sender, EventArgs e)  // modifica i vari colori 
        {
            ColorRandom c = new ColorRandom();
            Color color = c.GetRandomColorPreferences();
            Cerchio.BackgroundColor = color;
            CerchioRc.FillColor = color;
            CerchioRc.BackgroundColor = color;

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

