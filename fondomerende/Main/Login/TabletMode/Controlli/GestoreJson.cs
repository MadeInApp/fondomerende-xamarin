using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Foundation;
using System.Linq;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace fondomerende.Main.Login.TabletMode.Controlli
{
    class GestoreJson
    {
        LetturaFile letturafile;

        private string filename = "Utenti.json";
        public GestoreJson()
        {
            letturafile = new LetturaFile();
        }

        public async void serializza(List<Utente> u)
        {
            string json = JsonConvert.SerializeObject(u, Formatting.Indented);
            await letturafile.SaveAsync(filename, json);
        }

        public async Task<Utente[]> deserializza()
        {
            string json = await letturafile.LoadAsync(filename);
            Utente[] u = JsonConvert.DeserializeObject<Utente[]>(json);
            return u;
        }

    }  
}
