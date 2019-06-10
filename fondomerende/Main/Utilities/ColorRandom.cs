using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace fondomerende.Main.Utilities
{
    class ColorRandom
    {
        public Color GetRandomColor()
        {
            Random random = new Random();
            Color[] color = new Color[] { Color.FromHex("#FFFFFF"), Color.FromHex("#E01F25"), Color.FromHex("FFBF18"), Color.FromHex("41FF32"),
            Color.FromHex("00A000"), Color.FromHex("00C196"), Color.FromHex("#00323F") };

            //string[] color = new string[] { "#FFFFFF", "#E01F25", "#FFBF18", "#41FF32", "#00A000", "#00C196", "#00323F" };
            int numeroCasuale = random.Next(0, color.Length);

            Preferences.Set("Colore", GetHexString(color[numeroCasuale]));

            return color[numeroCasuale];
        }

        private static string GetHexString(Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
    }
}
