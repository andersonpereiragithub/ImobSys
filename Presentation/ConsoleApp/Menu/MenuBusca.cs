using System;
using ImobSys.Domain;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Interfaces;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuBusca
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IImovelRepository _imovelRepository;

        public MenuBusca(IClienteRepository clienteRepository, IImovelRepository imovelRepository)
        {
            _clienteRepository = clienteRepository;
            _imovelRepository = imovelRepository;
        }

        public void BuscarClientePorNome()
        {
            try
            {
                Console.Write("Digite o nome do cliente: ");
                var nome = Console.ReadLine();
                if (nome != null)
                {
                    Cliente cliente = _clienteRepository.BuscarPorNomeCliente(nome);  // Método hipotético em IClienteRepository
                    
                    if (cliente != null)
                    {
                        Console.WriteLine($"Cliente encontrado: {cliente.Nome}, CPF: {cliente.CPF}");
                    }
                    else
                    {
                        Console.WriteLine("Cliente não encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void BuscarImovelPorInscricaoIPTU()
        {
            Console.Write("Digite a Inscrição IPTU do imóvel: ");
            var inscricaoIptu = Console.ReadLine();
            Imovel imovel = _imovelRepository.BuscarPorInscricaoIPTU(inscricaoIptu);  // Método hipotético em IImovelRepository

            if (imovel != null)
            {
                Console.WriteLine($"Imóvel encontrado: Tipo {imovel}, Área: {imovel.AreaUtil} m²");
            }
            else
            {
                Console.WriteLine("Imóvel não encontrado.");
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
