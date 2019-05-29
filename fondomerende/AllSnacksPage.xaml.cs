using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Services.Models;

namespace fondomerende
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllSnacksPage : ContentPage
    {
        List<SnackDataDTO> AllSnacks = new List<SnackDataDTO>();

        SnackDataDTO Snack;
        public AllSnacksPage()
        {
            InitializeComponent();
            GetSnacksMethod();

            ListView.ItemsSource = AllSnacks;
            
            //ListView listView = new ListView
            //{
            //    ItemsSource = GetSnacks,

            //    ItemTemplate = new DataTemplate(() =>
            //    {
            //        Label nameLabel = new Label();
            //        nameLabel.SetBinding(Label.TextProperty, "token");
            //        return GetSnacks;
            //    })
                

            //};
        }
        public void GetSnacksMethod()
        {
            //   SnackServiceManager snackServiceManager = new SnackServiceManager();
            // await snackServiceManager.GetSnacksAsync();
            for (int i = 0; i < 5; i++)
            {
                AllSnacks.Add(new SnackDataDTO { id = i, friendly_name = "snack" + i.ToString() });
            }
            
        }

    }
}
