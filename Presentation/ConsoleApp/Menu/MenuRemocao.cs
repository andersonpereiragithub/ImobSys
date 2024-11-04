using System;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuRemocao
    {
        private readonly IClienteRepository<Cliente> _clienteRepository;
        private readonly IImovelRepository _imovelRepository;

        public MenuRemocao(IClienteRepository<Cliente> clienteRepository, IImovelRepository imovelRepository)
        {
            _clienteRepository = clienteRepository;
            _imovelRepository = imovelRepository;
        }

        //public void RemoverCliente()
        //{
        //    Console.Write("Digite o nome do cliente a ser removido: ");
        //    var nome = Console.ReadLine();
        //    var cliente = _clienteRepository.BuscarPorNomeCliente(nome);

        //    if (cliente == null)
        //    {
        //        Console.WriteLine("Cliente não encontrado.");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var removido = _clienteRepository.RemoverCliente(cliente.Id);
        //            Console.WriteLine(removido ? "Cliente removido com sucesso." : "Cliente não encontrado ou não pôde ser removido.");
        //        }
        //        catch (InvalidOperationException ex)
        //        {
        //            Console.WriteLine($"Erro: {ex.Message}");
        //        }
        //    }
        //    Console.WriteLine("Pressione qualquer tecla para continuar...");
        //    Console.ReadKey();
        //}

        public void RemoverImovel()
        {
            Console.Write("Digite a Inscrição IPTU do imóvel a ser removido: ");
            var inscricaoIptu = Console.ReadLine();
            var imovel = _imovelRepository.BuscarPorInscricaoIPTU(inscricaoIptu);

            if (imovel == null)
            {
                Console.WriteLine("Imóvel não encontrado.");
            }
            else
            {
                _imovelRepository.RemoverImovel(imovel.Id);
                Console.WriteLine("Imóvel removido com sucesso.");
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
