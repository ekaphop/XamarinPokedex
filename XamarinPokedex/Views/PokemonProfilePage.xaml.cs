using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinPokedex.ViewModels;

namespace XamarinPokedex.Views
{
    public partial class PokemonProfilePage : ContentPage
    {
        public PokemonProfilePage(string pokemonName)
        {
            InitializeComponent();
            BindingContext = new PokemonProfileViewModel(pokemonName);
        }
    }
}
