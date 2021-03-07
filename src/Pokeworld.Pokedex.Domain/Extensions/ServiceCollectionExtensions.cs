using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Pokeworld.Pokedex.Clients.Extensions;
using Pokeworld.Pokedex.Domain.Handlers;

namespace Pokeworld.Pokedex.Domain.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void AddDomainDependencies(this IServiceCollection services)
        {
            services.AddClientDependencies();
            services.AddScoped<IPokemonQueryHandler, PokemonQueryHandler>();
        }
    }
}
