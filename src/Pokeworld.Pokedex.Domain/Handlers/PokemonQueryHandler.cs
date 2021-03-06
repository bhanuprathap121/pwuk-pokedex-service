using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pokeworld.Pokedex.Contracts.Api.Responses;

namespace Pokeworld.Pokedex.Domain.Handlers
{
    public class PokemonQueryHandler : IPokemonQueryHandler
    {
        public PokemonQueryHandler()
        {
                
        }
        public async Task<BasicPokemonResponse> GetAsync(string name)
        {
            return new BasicPokemonResponse {Name = name};
        }
    }
}
