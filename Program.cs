using System;
using ImobSys.Application.Services;
using ImobSys.Domain.Interfaces;
using ImobSys.Infrastructure.Repositories;
using ImobSys.Presentation.ConsoleApp.Menu;

namespace ImobSys.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IImovelRepository imovelRepository = new JsonImovelRepository("imoveis.json");
            IClienteRepository clienteRepository = new JsonClienteRepository(imovelRepository, "clientes.json");
            
            ClienteService clienteService = new ClienteService(clienteRepository); //DÚVIDA SOBRE ISSO!
            ImovelService imovelService = new ImovelService(imovelRepository, clienteRepository);//DÚVIDA SOBRE ISSO!
            
            var menuPrincipal = new MenuPrincipal(clienteRepository, imovelRepository, clienteService, imovelService);

            menuPrincipal.Exibir();
        }
    }
}


