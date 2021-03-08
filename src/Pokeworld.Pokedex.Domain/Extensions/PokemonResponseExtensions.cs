using System;
using System.Linq;
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
                Description = pokemonSpeciesResponse.FlavorTextEntries.FirstOrDefault(IsEnglish())?.FlavorText.RemoveEscapeCharacters(),
                Habitat = pokemonSpeciesResponse.Habitat.Name,
                IsLegendary = pokemonSpeciesResponse.IsLegendary
            };
        }

        private static Func<FlavorTextEntry, bool> IsEnglish()
        {
            return t => t.Language.Name.ToLowerInvariant() == "en";
        }
    }
}
