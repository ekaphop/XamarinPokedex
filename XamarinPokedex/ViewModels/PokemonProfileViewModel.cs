using System;
using Xamarin.Forms;
using XamarinPokedex.Models;
using XamarinPokedex.Services;

namespace XamarinPokedex.ViewModels
{
    public class PokemonProfileViewModel
    {
        private PokemonProfileEntity _pokemonProfileEntity;

        

        

        public PokemonProfileViewModel(string pokemonName)
        {
            _pokemonProfileEntity = new PokemonProfileEntity();
            LoadData(pokemonName);
        }

        private async void LoadData(string pokemonName)
        {
            var PokemonProfileEntity = await PokeApiService.Instance.GetPokemonProfileByName(pokemonName);
            _pokemonProfileEntity = PokemonProfileEntity;

            //BindingPokemonMainList();
        }
    }
}
