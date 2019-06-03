using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using fondomerende.PostLoginPages;

namespace fondomerende
{
    class ColorRandom
    {
        public Color GetRandomColor()
        {
            Random random = new Random();
            Color[] color = new Color[] { Color.FromHex("#FFFFFF"), Color.FromHex("#E01F25"), Color.FromHex("FFBF18"), Color.FromHex("41FF32"),
            Color.FromHex("00A000"), Color.FromHex("00C196"), Color.FromHex("#00323F") };


            int numeroCasuale = random.Next(0, color.Length);

            return color[numeroCasuale];
        }
    }
}
