using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using fondomerende.PostLoginPages;

namespace fondomerende
{
    class LabelRandom
    {
        public String GetRandomPhrases()
        {
            Random random = new Random();
            string[] Phrase = new string[] { "Compilando le merendine...", "Clonando la password...", "", "These are not the droid you're looking for.", "Aggiungi del Testo Qui", "Powered by Apple Pro Stand" };
            int numeroCasuale = random.Next(0, Phrase.Length);
            return Phrase[numeroCasuale];
        }
    }
}
