using System.Text.Json.Serialization;

namespace Pokeworld.Pokedex.Clients.FunTranslationsApi.Contracts
{
    public class TranslationResponse
    {
        [JsonPropertyName("contents")]
        public Contents Contents { get; set; }
    }
    public class Contents
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }
    }
}
