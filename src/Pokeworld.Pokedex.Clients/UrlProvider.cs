using Microsoft.Extensions.Configuration;
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

    }
}