using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Login.TabletMode.Controlli
{
    class ControlloCodice
    {
        List<Utente> utenti;
        GestoreJson gestorejson;
        LoginServiceManager loginService;
        public ControlloCodice()
        {
            riempiLista();
        }

        private async void riempiLista()
        {
            Utente[] u = await gestorejson.deserializza();
            foreach (var app in u)
            {
                if(!utenti.Contains(app)) utenti.Add(app);
            }
        }
        public async void AggiungiUtente(Utente u)
        {
            if (ControllaRegistrato(u)) {
                utenti.Add(u);
            }
        }

        public bool ControllaRegistrato(Utente u)
        {

            foreach(var utente in utenti)
            {
                if (u.Username == utente.Username)
                {
                    return true;
                }
            }
            return false;
        }

        public bool VerificaCodice(string codice)
        {

            foreach (var u in utenti)
            {
                if (codice == u.Codiceunivoco)
                {
                    return true;
                }
            }
            return false;
        }

        public async System.Threading.Tasks.Task CambiaCodiceAsync(string username,string password, string codice)
        {
            bool trovato = false;
            var result = await loginService.LoginAsync(username, password, false);
            if (result.success)
            {
                foreach (var app in utenti)
                {
                    if(app.Username == username && app.Password == password)
                    {
                        app.Codiceunivoco = codice;
                        trovato = true;
                    }
                    break;
                }
            }
            if (trovato) { }
        }
    }
}
