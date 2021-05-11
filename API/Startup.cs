using System.Collections.Generic;
using API.Helpers;
using Main;
using Main.Navigation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var obstacles = new List<Coordinates>
            {
                new(2, 4),
                new(6, 7),
                new(7, 2)
            };
            services.AddSingleton<IAmTopology>(_ => new PlutoTopology(9, 9, obstacles));
            services.AddSingleton<IAmRover, Rover>();
            services.AddSingleton<IBuildCommands, CommandBuilder>();
            services.AddSingleton<IParseCommands, CommandParser>();
            services.AddSingleton<IControlNavigation, NavigationController>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}