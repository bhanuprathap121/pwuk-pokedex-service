using Microsoft.Extensions.DependencyInjection;
using Pokeworld.Pokedex.Clients.PokeApi.Queries;

namespace Pokeworld.Pokedex.Clients.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddClientDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPokeApiQueries, PokeApiQueries>();

        }
    }
}
