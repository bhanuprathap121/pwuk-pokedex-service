using System.Collections.Generic;
using System.Text;

namespace Pokeworld.Pokedex.Clients
{
    public interface IUrlProvider
    {
        public string GetPokemonUrl(string name);
    }
}
