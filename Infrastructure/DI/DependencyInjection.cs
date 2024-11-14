using Microsoft.Extensions.DependencyInjection;
using ImobSys.Infrastructure.Repositories;
using ImobSys.Domain.Interfaces;
using ImobSys.Application.Services;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Presentation.ConsoleApp.Menu;
using ImobSys.Domain.Entities.Clientes;

namespace ImobSys.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var clienteJsonPath = "clientes.json";
            var imovelJsonPath = "imoveis.json";


            // Repositórios
            services.AddSingleton<IImovelRepository>(provider => 
                new JsonImovelRepository(imovelJsonPath));
            services.AddSingleton<IClienteRepository<Cliente>>(provider => 
                new JsonClienteRepository<Cliente>(clienteJsonPath));

            // Serviços
            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<IImovelService, ImovelService>();

            // Menus
            services.AddTransient<MenuCadastro>();
            services.AddTransient<MenuBusca>();
            services.AddTransient<ConsoleDataLister>();
            services.AddTransient<MenuListagemOperacoes>();
            services.AddTransient<MenuRemocao>();
            services.AddTransient<MenuPrincipal>();

            return services;
        }
    }
}
