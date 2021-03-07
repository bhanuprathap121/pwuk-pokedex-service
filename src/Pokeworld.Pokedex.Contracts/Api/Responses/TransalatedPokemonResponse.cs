namespace Pokeworld.Pokedex.Contracts.Api.Responses
{
    public class TranslatedPokemonResponse
    {
        public string Name { get; set; }
        public string TransalatedDescription { get; set; }
        public string Habitat { get; set; }
        public bool IsLegendary { get; set; }   
    }
}
