using ImobSys.Application.Services;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using ImobSys.Infrastructure.Repositories;
using ImobSys.Presentation.ConsoleApp.Handler;
using ImobSys.Presentation.ConsoleApp.Handlers;
using ImobSys.Presentation.ConsoleApp.Menu;
using Microsoft.Extensions.DependencyInjection;

namespace ImobSys.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Registrar Repositórios (com caminhos configurados)
            services.AddSingleton<IImovelRepository>(provider =>
                new JsonImovelRepository("imoveis.json"));
            services.AddSingleton<IClienteRepository<Cliente>>(provider =>
                new JsonClienteRepository<Cliente>("clientes.json"));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar Serviços de Aplicação
            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<IImovelService, ImovelService>();

            return services;
        }

        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddSingleton<InputHandler>();
            services.AddSingleton<OutputHandler>();

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
