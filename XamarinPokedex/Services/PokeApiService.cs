using System;
using XamarinPokedex.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rest;

namespace XamarinPokedex.Services
{
    public class PokeApiService
    {
        private string _pokemonListUrl = "https://pokeapi.co/api/v2/pokemon?offset={0}&limit={1}";
        private string _pokemonProfileUrl = "https://pokeapi.co/api/v2/pokemon/{0}/";
        private string _pokemonSpeciesUrl = "https://pokeapi.co/api/v2/pokemon-species/{0}/";

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

        public async Task<PokemonProfileEntity> GetPokemonProfile(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var url = string.Format(_pokemonProfileUrl, id);
                    Uri uri = new Uri(url);
                    var result = await client.GetStringAsync(uri);
                    return JsonConvert.DeserializeObject<PokemonProfileEntity>(result);
                }
                catch (Exception ex)
                {
                    return new PokemonProfileEntity();
                }
            }
        }

        public async Task<PokemonSpeciesEntity> GetPokemonSpecies(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var url = string.Format(_pokemonSpeciesUrl, id);
                    Uri uri = new Uri(url);
                    var result = await client.GetStringAsync(uri);
                    return JsonConvert.DeserializeObject<PokemonSpeciesEntity>(result);
                }
                catch (Exception ex)
                {
                    return new PokemonSpeciesEntity();
                }
            }
        }

        public async Task<PokemonChainEntity> GetPokemonChain(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    Uri uri = new Uri(url);
                    var result = await client.GetStringAsync(uri);
                    return JsonConvert.DeserializeObject<PokemonChainEntity>(result);
                }
                catch (Exception ex)
                {
                    return new PokemonChainEntity();
                }
            }
        }
    }
}