using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;
using Pokeworld.Pokedex.Contracts.Api.Responses;

namespace Pokeworld.Pokedex.Domain.Extensions
{
    public static class PokemonResponseExtensions
    {
        public static BasicPokemonResponse ToBasicPokemonResponse(this PokemonResponse pokemonResponse, PokemonSpeciesResponse pokemonSpeciesResponse)
        {
            return new BasicPokemonResponse
            {
                Name = pokemonResponse.Name,
                Description = pokemonSpeciesResponse.FlavorTextEntries.FirstOrDefault()?.FlavorText,
                Habitat = pokemonSpeciesResponse.Habitat.Name,
                IsLegendary = pokemonSpeciesResponse.IsLegendary
            };
        }

    }
}
