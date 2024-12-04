using Microsoft.Extensions.DependencyInjection;
using ImobSys.Infrastructure.DI;

var serviceCollection = new ServiceCollection();
serviceCollection.AddInfrastructure();
serviceCollection.AddApplicationServices();
serviceCollection.AddPresentation();

var serviceProvider = serviceCollection.BuildServiceProvider();

Console.SetWindowSize(103, 40);
Console.SetBufferSize(110, 40);

var menuPrincipal = serviceProvider.GetRequiredService<MenuPrincipal>();
menuPrincipal.Exibir();
