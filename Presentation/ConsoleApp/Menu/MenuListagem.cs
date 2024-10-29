using System;
using ImobSys.Domain.Interfaces;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuListagem
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IImovelRepository _imovelRepository;

        public MenuListagem(IClienteRepository clienteRepository, IImovelRepository imovelRepository)
        {
            _clienteRepository = clienteRepository;
            _imovelRepository = imovelRepository;
        }

        public void ListarTodosClientes()
        {
            var clientes = _clienteRepository.ListarTodosCliente();
            if (clientes.Count > 0)
            {
                Console.WriteLine("\u001b[33m\nClientes cadastrados:\u001b[0m");
                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"Nome: {cliente.Nome}, CPF: {cliente.CPF}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void ListarTodosImoveis()
        {
            var imoveis = _imovelRepository.ListarTodosImovel();
            if (imoveis.Count > 0)
            {
                Console.WriteLine("Imóveis cadastrados:");
                foreach (var imovel in imoveis)
                {
                    Console.WriteLine($"ID: {imovel.Id}, Tipo: {imovel.TipoImovel}, Área: {imovel.AreaUtil} m²");
                }
            }
            else
            {
                Console.WriteLine("Nenhum imóvel cadastrado.");
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
