﻿using fondomerende.Services.RESTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fondomerende.PostLoginPages.GraphicInterfaces
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChronologyContentPage : ContentPage
    {
        string[] cronologia;
        public ChronologyContentPage()
        {
            InitializeComponent();
            switch (Device.RuntimePlatform)             //Se il dispositivo è Android non mostra la Top Bar della Navigation Page, se è iOS la mostra
            {
                default:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;

            }
            AddAction();
            AddTimeLine();
        }



        private void AddAction()
        {
            var stackPrincipale = new StackLayout
            {
                BackgroundColor = Color.Red
            };

            var cerchio = new Frame
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                BorderColor = Color.Black,
                HasShadow = true,
                HeightRequest = 15,
                WidthRequest = 15,
                CornerRadius = 32,
            };

            var stackLabel = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
            };

            var firstLetter = new Label
            {
                Text="C",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };

            var textAction = new Label
            {
                Text = "Ha mangiato",
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
            };

            stackPrincipale.Children.Add(cerchio);
            stackPrincipale.Children.Add(textAction);
            cerchio.Content = stackLabel;
            stackLabel.Children.Add(firstLetter);


            
            ContentLayout.Children.Add(stackPrincipale);
        }

        public void AddTimeLine()
        {
            var stackPrincipale = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.Blue

            };

            var linea = new Frame
            {
                HasShadow = true,
                HeightRequest = 20,
                WidthRequest = 5,
            };

            var orario = new Label
            {
                Text = "1h 37min",
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
            };

            stackPrincipale.Children.Add(linea);
            stackPrincipale.Children.Add(orario);


            ContentLayout.Children.Add(stackPrincipale);
        }

        //public void First_letter()        //Grafica
        //{
        //    string firstLetter = ;

        //    string[] strSplit = //array da passare .Split();

        //    foreach (string res in strSplit)
        //    {
        //        firstLetter = (res.Substring(0, 1));
        //    }
        //    inizialeLabel.Text = firstLetter;
        //}
        public async void GetLastActions() //roba non funzionante
        {
            LastActionServiceManager lastAction = new LastActionServiceManager();
            var result = await lastAction.GetLastActions();

            if (result.response.success == true)
            {
                cronologia = result.data.actions;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
            }
        }
    }
}