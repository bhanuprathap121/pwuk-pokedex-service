namespace Pokeworld.Pokedex.Clients
{
    public interface IUrlProvider
    {
        public string GetPokemonUrl(string name);
        public string GetYodaTranslationUrl(string text);
        public string GetShakespeareTranslationUrl(string text);
    }
}
