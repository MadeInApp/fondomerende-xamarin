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

        public EditUserViewCell()
        {
            InitializeComponent();
            First_letter();
            friendly_name.Text = InformationFriendlyName();
            Cerchio.BackgroundColor = Color.FromHex(Preferences.Get("Colore", "#CCCCCC"));
          //  GetFundsMethod();

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

            LogoutViewCell logoutView = new LogoutViewCell();
            logoutView.SetImageColor(color);


            //Preferences.Set("Colore", "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"));
        }


        public async void GetFundsMethod()
        {
            UserFundsServiceMangaer UserFundsService = new UserFundsServiceMangaer();

            var result = await UserFundsService.GetUserFunds();

            if(result.response.success)
            {
                username.Text = result.data.user_funds_amount;
            }
            else
            {
               //
            }


        }
    }
}

