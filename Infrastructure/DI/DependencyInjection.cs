using ImobSys.Application.Services;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using ImobSys.Infrastructure.Repositories;
using ImobSys.Presentation.Handler;
using ImobSys.Presentation.Menu;
using Microsoft.Extensions.DependencyInjection;

namespace ImobSys.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            string clienteJsonPath = "clientes.json";
            string imovelJsonPath = "imoveis.json";

            services.AddSingleton<IImovelRepository>(provider =>
                       new JsonImovelRepository(imovelJsonPath));
            services.AddSingleton<IClienteRepository<Cliente>>(provider =>
                       new JsonClienteRepository<Cliente>(clienteJsonPath));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<IImovelService, ImovelService>();

            return services;
        }

        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddSingleton<UserInteractionHandler>();

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
