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
    public partial class Frame : ViewCell
    {
        public string firstLetterIcon = "dd";
        public string friendly_name = Preferences.Get("username", null);
        public Frame()
        {
            InitializeComponent();
            firstLetterIcon = First_letter();
            friendly_name = InformationFriendlyName();
        }

        public string First_letter()        //Grafica
        {
            string firstLetter = "";
            Preferences.Get("friendly-name", "");

            string[] strSplit = Preferences.Get("friendly-name", "").Split();

            foreach (string res in strSplit)
            {
                firstLetter = (res.Substring(0, 1));
            }
            return firstLetter;
        }

        public string InformationFriendlyName() => Preferences.Get("friendly-name", "");
    }
}