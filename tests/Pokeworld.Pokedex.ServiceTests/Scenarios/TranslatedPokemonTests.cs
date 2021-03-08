using System.Threading.Tasks;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Pokeworld.Pokedex.Contracts.Api.Responses;

namespace Pokeworld.Pokedex.ServiceTests.Scenarios
{
    public partial class TranslatedPokemonTests
    {
        private readonly BasicPokemonResponse _mewtwoPokemon = new BasicPokemonResponse
        {
            Name = "mewtwo",
            Habitat = "rare",
            IsLegendary = true
        };
        private readonly BasicPokemonResponse _charmeleonPokemon = new BasicPokemonResponse
        {
            Name = "charmeleon",
            Habitat = "mountain",
            IsLegendary = false
        };
       
        private readonly string[] _mewtwoDescriptions = {
            "Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.",
            "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments."
        };

        private readonly string[] _charmeleonDescriptions = {
            "At which hour it swings its burning tail,  it elevates the temperature to unbearably high levels.",
            "When it swings its burning tail, it elevates the temperature to unbearably high levels."
        };


        [Scenario]
        public async Task Get_yoda_translated_or_standard_pokemon_success()
        {
            await Runner.RunScenarioAsync(
                given => As_a_Pokeworld_user_who_has_access_to_the_api(),
                when => I_am_requesting_the_translated_pokemon_details(_mewtwoPokemon.Name),
                then => I_should_see_the_translated_pokemon_details(_mewtwoDescriptions, _mewtwoPokemon));
        }

        [Scenario]
        public async Task Get_shakespeare_translated_or_standard_pokemon_success()
        {
            await Runner.RunScenarioAsync(
                given => As_a_Pokeworld_user_who_has_access_to_the_api(),
                when => I_am_requesting_the_translated_pokemon_details(_charmeleonPokemon.Name),
                then => I_should_see_the_translated_pokemon_details(_charmeleonDescriptions, _charmeleonPokemon));
        }
    }
}
