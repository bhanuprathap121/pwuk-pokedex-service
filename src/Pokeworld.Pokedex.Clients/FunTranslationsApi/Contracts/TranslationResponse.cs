using System;
using System.Collections.Generic;
using System.Text;
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
        [JsonPropertyName("translation")]
        public string Translation { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("translated")]
        public string Translated { get; set; }
    }
}
