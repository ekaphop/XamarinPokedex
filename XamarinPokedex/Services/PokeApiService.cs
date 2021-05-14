using System;
using XamarinPokedex.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamarinPokedex.Services
{
    public class PokeApiService
    {
        private string _pokemonListUrl = "https://pokeapi.co/api/v2/pokemon?offset={0}&limit={1}";
        private string _pokemonProfileUrl = "https://pokeapi.co/api/v2/pokemon/{0}";

        private static PokeApiService _instance;
        public static PokeApiService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PokeApiService();
                return _instance;
            }
        }

        public async Task<PokemonMainEntity> GetPokemonLists(int offset,int limit)
        {
            using (var client = new HttpClient())
            {
                var uri = string.Format(_pokemonListUrl, offset, limit);
                var result = await client.GetStringAsync(uri);

                return JsonConvert.DeserializeObject<PokemonMainEntity>(result);
            }
        }

        public async Task<PokemonProfileEntity> GetPokemonProfile(int pokemonId)
        {
            using (var client = new HttpClient())
            {
                var uri = string.Format(_pokemonProfileUrl, pokemonId);
                var result2 = await client.GetStreamAsync(uri);
                var result = await client.GetStringAsync(uri);

                //return JsonConvert.DeserializeObject<PokemonProfileEntity>(result);

                return new PokemonProfileEntity();
            }
        }

        public async Task<PokemonProfileEntity> GetPokemonProfileByName(string pokemonName)
        {
            using (var client = new HttpClient())
            {
                var uri = string.Format(_pokemonProfileUrl, pokemonName);
                var result = await client.GetStringAsync(uri);

                //return JsonConvert.DeserializeObject<PokemonProfileEntity>(result);

                return new PokemonProfileEntity();
            }
        }
    }
}