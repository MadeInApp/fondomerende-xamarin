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
        public string friendly_name;
        public EditUserViewCell()
        {
            InitializeComponent();

        InitializeComponent();
        firstLetterIcon = First_letter();
        friendly_name = InformationFriendlyName();
    }


    public string First_letter()        //Grafica
    {
            string firstLetter;
            Preferences.Get("friendly_name", "");

            string[] strSplit = Preferences.Get("friendly_name", "").Split();

            foreach (string res in strSplit)
            {
                firstLetter = (res.Substring(0, 1));
            }
            return firstLetter;
        }

        public string InformationFriendlyName() => Preferences.Get("friendly_name", "");
    }
}
