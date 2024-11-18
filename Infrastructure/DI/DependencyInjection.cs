using ImobSys.Application.Services;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using ImobSys.Infrastructure.Repositories;
using ImobSys.Presentation.ConsoleApp.Menu;
using Microsoft.Extensions.DependencyInjection;

namespace ImobSys.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var clienteJsonPath = "clientes.json";
            var imovelJsonPath = "imoveis.json";

            services.AddSingleton<IImovelRepository>(provider => 
                new JsonImovelRepository(imovelJsonPath));
            services.AddSingleton<IClienteRepository<Cliente>>(provider => 
                new JsonClienteRepository<Cliente>(clienteJsonPath));

            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<IImovelService, ImovelService>();

            services.AddTransient<MenuCadastro>();
            services.AddTransient<MenuBusca>();
            services.AddTransient<ConsoleDataLister>();
            services.AddTransient<MenuDeListas>();
            services.AddTransient<MenuRemocao>();
            services.AddTransient<MenuPrincipal>();

            return services;
        }
    }
}
