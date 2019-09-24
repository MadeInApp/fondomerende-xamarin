using System;
using System.Collections.Generic;
using System.Text;

namespace fondomerende.Main.Login.TabletMode.Controlli
{

    class Utente
    {
        string username, password, codiceunivoco;

        public Utente(string username, string password, string codiceunivoco)
        {
            this.username = username;
            this.password = password;
            this.codiceunivoco = codiceunivoco;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Codiceunivoco { get; set; }

    }
}
