using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using fondomerende.PostLoginPages;

namespace fondomerende.Main.Utilities
{
    class LabelRandom
    {
        public String GetRandomPhrases()
        {
            Random random = new Random();
            string[] Phrase = new string[] { "Good to See you, " + Preferences.Get("friendly-name",null) + "!", "Welcome to the wonderfully edible world of Fondo Merende", "Rimettendo gli snack al loro posto...", "Compilando le merendine...", "Clonando la carta di credito...", "", "These are not the droid you're looking for", "Aggiungi del testo qui", "Powered by Apple ProStand"};
            int numeroCasuale = random.Next(0, Phrase.Length);
            return Phrase[numeroCasuale];
        }
    }
}
