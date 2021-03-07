using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokeworld.Pokedex.Clients;
using Pokeworld.Pokedex.Domain;
using Pokeworld.Pokedex.Domain.Extensions;

namespace Pokeworld.Pokedex.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const int RetryCount = 3;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ServiceUrls>(Configuration.GetSection(ServiceUrls.Services));
            services.AddHttpClient<IClient, Client>();
            services.AddDomainDependencies();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokeworld Pokedex Api v1");
            });
        }
    }
}
