using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace fondomerende.Main.Login.TabletMode.Controlli
{
    class LetturaFile
    {
        public async Task SaveAsync(string filename, string text)
        {
            string path = CreatePathToFile(filename);
            using (StreamWriter sw = File.CreateText(path))
                await sw.WriteAsync(text);
        }
        public async Task<string> LoadAsync(string filename)
        {
            string path = CreatePathToFile(filename);
            using (StreamReader sr = File.OpenText(path))
                return await sr.ReadToEndAsync();
        }

        public bool FileExists(string filename)
        {
            return File.Exists(CreatePathToFile(filename));
        }
        private string CreatePathToFile(string filename)
        {
            string risultato = "";
            switch (Device.RuntimePlatform)
            {
                case (Device.Android):
                    var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    risultato = Path.Combine(docsPath, filename);
                    break;
                case (Device.iOS):
                    risultato = Path.Combine(DocumentsPath, filename);
                    break;
            }
            return risultato;
        }

        public static string DocumentsPath
        {
            get
            {
                var documentsDirUrl = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User).Last();
                return documentsDirUrl.Path;
            }
        }


    }
}

