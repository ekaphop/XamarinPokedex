using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinPokedex.Models;
using XamarinPokedex.ViewModels;

namespace XamarinPokedex.Views
{
    public partial class PokemonMainPage : ContentPage
    {
        private TimeSpan interval;

        public PokemonMainPage()
        {
            InitializeComponent();
            BindingContext = new PokemonMainViewModel(Navigation);
        }

        public void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            (BindingContext as PokemonMainViewModel).LoadMoreItems(e.Item as DoubleGridItem);
        }
    }
}