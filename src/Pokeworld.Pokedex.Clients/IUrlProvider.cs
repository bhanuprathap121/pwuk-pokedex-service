namespace Pokeworld.Pokedex.Clients
{
    public interface IUrlProvider
    {
        public string GetPokemonUrl(string name);
    }
}
