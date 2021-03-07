using System.Text.Json.Serialization;

namespace Pokeworld.Pokedex.Clients.PokeApi.Contracts
{
    public class PokemonResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("species")]
        public Species Species { get; set; }
        
    }
    public class Species
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
