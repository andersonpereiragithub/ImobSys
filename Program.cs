using Microsoft.Extensions.DependencyInjection;
using ImobSys.Infrastructure.DI;

var serviceCollection = new ServiceCollection();
serviceCollection.AddInfrastructure();

var serviceProvider = serviceCollection.BuildServiceProvider();

var menuPrincipal = serviceProvider.GetRequiredService<MenuPrincipal>();
menuPrincipal.Exibir();



