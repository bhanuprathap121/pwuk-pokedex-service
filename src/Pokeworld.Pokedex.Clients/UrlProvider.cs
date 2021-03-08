using System.Web;
using Microsoft.Extensions.Options;

namespace Pokeworld.Pokedex.Clients
{
    public class UrlProvider : IUrlProvider
    {
        private readonly ServiceUrls _serviceUrls;
        public UrlProvider(IOptions<ServiceUrls> options)
        { 
            _serviceUrls = options.Value;
        }

        public string GetPokemonUrl(string name)
        {
            return $"{_serviceUrls.PokeApiUrl}/{name}";
        }

        public string GetYodaTranslationUrl(string text)
        {
            return $"{_serviceUrls.FunTranslationApiUrl}/{_serviceUrls.YodaPath}?text={HttpUtility.UrlEncode(text)}";
        }
        public string GetShakespeareTranslationUrl(string text)
        {
            return $"{_serviceUrls.FunTranslationApiUrl}/{_serviceUrls.ShakespearePath}?text={HttpUtility.UrlEncode(text)}";
        }

    }
}