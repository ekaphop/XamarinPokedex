using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinPokedex.Models;
using XamarinPokedex.ViewModels;

namespace XamarinPokedex.Views
{
    public partial class PokemonProfilePage : ContentPage
    {
        public PokemonProfilePage(PokemonProfileEntity pokemonProfile
            , PokemonSpeciesEntity pokemonSpecies
            , List<ItemEntity> pokemonChain)
        {
            InitializeComponent();
            var vm = new PokemonProfileViewModel(pokemonProfile, pokemonSpecies, pokemonChain);
            BindingContext = vm;
        }
    }
}
