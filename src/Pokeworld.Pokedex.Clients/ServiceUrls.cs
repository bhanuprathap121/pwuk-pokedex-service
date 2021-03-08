using System.Diagnostics.CodeAnalysis;

namespace Pokeworld.Pokedex.Clients
{
    [ExcludeFromCodeCoverage]
    public class ServiceUrls
    {
        public const string Services = "Services";
        public string PokeApiUrl { get; set; }
        public string FunTranslationApiUrl { get; set; }
        public string ShakespearePath { get; set; }
        public string YodaPath { get; set; }
    }
}
