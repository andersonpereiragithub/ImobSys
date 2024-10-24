using Microsoft.Extensions.DependencyInjection;
using ImobSys.Infrastructure.DI;
using System;

namespace ImobSys.Presentation.ConsoleApp
{
    internal class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection(); 

            serviceCollection.AddInfrastructure();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
