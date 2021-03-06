using Microsoft.Extensions.DependencyInjection;
using Pokeworld.Pokedex.Domain.Handlers;

namespace Pokeworld.Pokedex.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomainDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPokemonQueryHandler, PokemonQueryHandler>();
        }
    }
}
