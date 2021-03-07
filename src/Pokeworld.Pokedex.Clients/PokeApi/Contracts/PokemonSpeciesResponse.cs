using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokeworld.Pokedex.Clients.PokeApi.Contracts
{
    public class PokemonSpeciesResponse
    {
        [JsonPropertyName("flavor_text_entries")]
        public List<FlavorTextEntry> FlavorTextEntries { get; set; }

        [JsonPropertyName("habitat")]
        public Habitat Habitat { get; set; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }

    }

    public class Habitat
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class FlavorTextEntry
    {
        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; set; }

        [JsonPropertyName("language")]
        public Language Language { get; set; }
    }

    public class Language
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
