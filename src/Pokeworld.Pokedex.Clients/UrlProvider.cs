using System;

namespace Pokeworld.Pokedex.Clients
{
    public class UrlProvider : IUrlProvider
    {
        public string GetPokemonUrl(string name)
        {
            return $"https://pokeapi.co/api/v2/pokemon/{name}";
        }

    }
}