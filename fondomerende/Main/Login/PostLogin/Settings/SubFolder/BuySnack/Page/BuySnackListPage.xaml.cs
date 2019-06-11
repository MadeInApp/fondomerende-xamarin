using fondomerende.Main.Services.RESTServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fondomerende.Main.Services.Models;
using Rg.Plugins.Popup.Extensions;
using fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Popup;
using fondomerende.Main.Utilities;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.BuySnack.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySnackListPage : ContentPage
    {
        public static int[] SelectedSnackIDArr;
        public static int SelectedSnackID;
        public static bool Refresh = false;
        SnackServiceManager SnackService = new SnackServiceManager();
        public ObservableCollection<string> Items { get; set; }

        public BuySnackListPage()
        {
            InitializeComponent();
            GetSnacksMethod();
            


            switch (Device.RuntimePlatform)                                                     //
            {                                                                                   //                                    
                                                                                                //   Se il dispositivo è Android non mostra la Top Bar della Navigation Page,
                case Device.Android:                                                            //      Se è iOS invece si (perchè senza è una schifezza)
                    NavigationPage.SetHasNavigationBar(this, false);                            //
                    break;                                                                      //
                                                                                                //
                default:                                                                    //
                    NavigationPage.SetHasNavigationBar(this, true);                             //
                    break;                                                                      //
            }                                                                                   //

            

        }

        public async Task GetSnacksMethod()     //ottiene la lista degli snack e la applica alla ListView           !CHIEDERE A LAPO DI AGGIUNGERE UN PARAMETRO "RESOLUTION" ALLA RICERCA!
        {
            var result = await SnackService.GetToBuySnacksAsync();
            ListView.ItemsSource = result.data.snacks;
            int z;
            for (int i = 0; i <= result.data.snacks.Count; i++)
            {
                ColorRandom rd = new ColorRandom();
                if (i % 2 == 0)
                {
                    z = i;
                    SelectedSnackIDArr[i] = z;
                    var StackLayout_z = new StackLayout{ Spacing = 10, HeightRequest = 100};
                    var imageButton_z = new ImageButton
                    {
                        Margin = new Thickness(0,20,0,20),
                        Scale = 2,
                        Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[z].friendly_name.Replace(" ", "&nbsp;") + "_500x500"
                    };

                    var label_z = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = result.data.snacks[z].friendly_name
                    };


                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            imageButton_z.BackgroundColor = Color.Transparent;
                            break;
                    }

                    imageButton_z.Clicked += OnImageButtonClicked;
                    StackLayout_z.Children.Add(imageButton_z);
                    StackLayout_z.Children.Add(label_z);
                    Column0.Children.Add(StackLayout_z);
                }
                else
                {
                    SelectedSnackIDArr[i] = i;
                    var StackLayout_i = new StackLayout { Spacing = 10, HeightRequest=100 };

                    var imageButton_i = new ImageButton
                    {
                        ScaleY=1,
                        ScaleX=1,
                        Margin = new Thickness(0, 20, 0, 20),
                        Scale = 2,
                        Source = "http://192.168.0.175:8888/fondomerende/public/getphoto.php?name=" + result.data.snacks[i].friendly_name.Replace(" ", "")
                    };

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            imageButton_i.BackgroundColor = Color.Transparent;
                            break;
                    }

                    var label_i = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = result.data.snacks[i].friendly_name
                    };

                    imageButton_i.Clicked += OnImageButtonClicked;
                    StackLayout_i.Children.Add(imageButton_i);
                    StackLayout_i.Children.Add(label_i);
                    Column1.Children.Add(StackLayout_i);
                }
            }
        }


        private async void OnImageButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new BuySnackPopUpPage());
        }


        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var result = await SnackService.GetToBuySnacksAsync();
            SelectedSnackID = (e.SelectedItem as ToBuyDataDTO).id;
            await Navigation.PushPopupAsync(new BuySnackPopUpPage());

        }

        private void Swap_Clicked(object sender, EventArgs e)
        {
            if(ScrollView.IsVisible == true)
            {
                Swap.BackgroundColor = Color.Orange;
                ScrollView.IsVisible = false;
                ListView.IsVisible = true;
            }
            else
            {
                Swap.BackgroundColor = Color.Transparent;
                ListView.IsVisible = false;
                ScrollView.IsVisible = true;
            }
        }
    }

}

