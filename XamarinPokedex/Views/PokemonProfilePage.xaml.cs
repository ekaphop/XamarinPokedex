using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinPokedex.Models;
using XamarinPokedex.ViewModels;

namespace XamarinPokedex.Views
{
    public partial class PokemonProfilePage : ContentPage
    {
        PokemonProfileViewModel vm = new PokemonProfileViewModel(null,null);

        public PokemonProfilePage(PokemonProfileEntity pokemonProfile, PokemonSpeciesEntity pokemonSpecies)
        {
            InitializeComponent();
            vm = new PokemonProfileViewModel(pokemonProfile, pokemonSpecies);
            BindingContext = vm;
        }
    }
}
