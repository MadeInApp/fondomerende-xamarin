using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Services.RESTServices;

namespace fondomerende.GraphicInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserViewCell : ViewCell
    {
        public string firstLetterIcon;
        string x;

        public EditUserViewCell()
        {
            InitializeComponent();
          
            First_letter();
            friendly_name.Text = InformationFriendlyName();
            Cerchio.BackgroundColor = Color.FromHex(Preferences.Get("Colore", "#CCCCCC"));
            FundsAmmount.Text = x;  // da sistemare

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

        [Obsolete]
        public async void Bottone_ClickedAsync(object sender, EventArgs e)  // modifica i vari colori 
        {
            ColorRandom c = new ColorRandom();
            Color color = c.GetRandomColor();
            Cerchio.BackgroundColor = color;

            LogoutViewCell logoutView = new LogoutViewCell();
            logoutView.SetImageColor(color);


            //Preferences.Set("Colore", "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"));
        }

        public async void GetFundsAsync()     //ottiene la lista degli snack e la applica alla ListView
        {
            UserFundsServiceManager funds = new UserFundsServiceManager();
            var resultFunds = await funds.GetUserFunds();

            x = resultFunds.data + "€";          
        }
    }
}

