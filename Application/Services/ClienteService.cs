using System;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Interfaces;

namespace ImobSys.Application.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public void CadastrarNovoCliente()
        {
            Console.Clear();
            Console.WriteLine("==== Cadastro de Novo Cliente ====");

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("CPF: ");
            string cpf = Console.ReadLine();

            Console.Write("Tipo do Cliente (Proprietário/Locatário): ");
            string tipoCliente = Console.ReadLine();

            var novoCliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                CPF = cpf,
                TipoCliente = tipoCliente
            };

            _clienteRepository.SalvarCliente(novoCliente);
            Console.WriteLine("Cliente cadastrado com sucesso!");
            Console.WriteLine("\nPressione qualquer tecla para retornar ao menu...");
            Console.ReadKey();
        }
    }
}
