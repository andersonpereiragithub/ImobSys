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
            // Especificando o tipo de cliente (PessoaFisica) para o repositório
            IImovelRepository imovelRepository = new JsonImovelRepository("imoveis.json");
            IClienteRepository<Cliente> clienteRepository = new JsonClienteRepository<Cliente>("clientes.json");

            // Passando repositórios específicos para os serviços
            ClienteService clienteService = new ClienteService(clienteRepository);
            ImovelService imovelService = new ImovelService(imovelRepository, clienteRepository);

            var menuPrincipal = new MenuPrincipal(clienteRepository, imovelRepository, clienteService, imovelService);

            menuPrincipal.Exibir();
        }
    }
}


