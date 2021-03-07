namespace Pokeworld.Pokedex.Contracts.Api.Responses
{
    public class BasicPokemonResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Habitat { get; set; }
        public bool IsLegendary { get; set; }   
    }
}
