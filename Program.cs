using ImobSys.Presentation.ConsoleApp.Menu;
using ImobSys.Infrastructure.Repositories;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Application.Services;
using ImobSys.Domain.Interfaces;
using System;

namespace ImobSys.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IImovelRepository imovelRepository = new JsonImovelRepository("imoveis.json");
            IClienteRepository<Cliente> clienteRepository = new JsonClienteRepository<Cliente>("clientes.json");

            ClienteService clienteService = new ClienteService(clienteRepository);
            ImovelService imovelService = new ImovelService(imovelRepository, clienteRepository);
            
            var menuCadastro = new MenuCadastro(clienteService, imovelService);
            var menuBusca = new MenuBusca(clienteRepository, imovelRepository);
            var menuListagem = new ConsoleDataLister(clienteRepository, imovelRepository);
            var menuRemocao = new MenuRemocao(clienteRepository, imovelRepository);

            var menuSecundarioListagem = new MenuListagemOperacoes(menuCadastro, menuBusca, menuListagem, menuRemocao);

            var menuPrincipal = new MenuPrincipal(menuSecundarioListagem, clienteRepository, imovelRepository, clienteService, imovelService);

            menuPrincipal.Exibir();
        }
    }
}


