using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinPokedex.Models
{
    public class PokemonResultEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
