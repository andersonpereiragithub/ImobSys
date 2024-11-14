using Microsoft.Extensions.DependencyInjection;
using ImobSys.Infrastructure.DI;

namespace ImobSys.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddInfrastructure();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var menuPrincipal = serviceProvider.GetRequiredService<MenuPrincipal>();
            menuPrincipal.Exibir();
        }
    }
}


