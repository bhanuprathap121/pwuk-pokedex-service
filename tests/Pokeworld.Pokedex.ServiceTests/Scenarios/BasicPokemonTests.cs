using System.Threading.Tasks;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Pokeworld.Pokedex.Contracts.Api.Responses;

namespace Pokeworld.Pokedex.ServiceTests.Scenarios
{
    [FeatureDescription("Api endpoint returns standard pokemon description and additional information")]
    [Label("Endpoint 1")]
    public partial class BasicPokemonTests
    {
        private readonly BasicPokemonResponse _mewtwoPokemonResponse = new BasicPokemonResponse
        {
            Name = "mewtwo",
            Habitat = "rare",
            IsLegendary = true,
            Description =
                "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments."
        };

        private const string NonPokemonName = "random";
        
        [Scenario]
        public async Task Get_basic_pokemon_success()
        {
            await Runner.RunScenarioAsync(
                given => As_a_Pokeworld_user_who_has_access_to_the_api(),
                when => I_am_requesting_the_basic_pokemon_details(_mewtwoPokemonResponse.Name),
                then => I_should_see_the_basic_pokemon_details());
        }

        [Scenario]
        public async Task Get_basic_pokemon_fail()
        {
            await Runner.RunScenarioAsync(
                given => As_a_Pokeworld_user_who_has_access_to_the_api(),
                when => I_am_requesting_the_basic_pokemon_details(NonPokemonName),
                then => I_should_get_not_found_response());
        }
    }
}