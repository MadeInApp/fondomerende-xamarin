using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserViewCell : ViewCell
    {
        public string firstLetterIcon;
        public string friendly_name = Preferences.Get("username", null);
        public EditUserViewCell()
        {
            InitializeComponent();
            First_letter();
            friendly_name = InformationFriendlyName();

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
    }
}

