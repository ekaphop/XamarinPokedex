using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPokedex.Views;
using XamarinPokedex.Views.TestPage;

namespace XamarinPokedex
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new PokemonMainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
