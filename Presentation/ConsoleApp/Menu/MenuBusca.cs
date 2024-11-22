using System.Runtime.ConstrainedExecution;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using ImobSys.Domain;
using System;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuBusca
    {
        private readonly IClienteRepository<Cliente> _clienteRepository;
        private readonly IImovelRepository _imovelRepository;

        public MenuBusca(IClienteRepository<Cliente> clienteRepository, IImovelRepository imovelRepository)
        {
            _clienteRepository = clienteRepository;
            _imovelRepository = imovelRepository;
        }

        public Guid BuscarClientePorNome(string nomeCliente)
        {
            if (string.IsNullOrWhiteSpace(nomeCliente))
            {
                throw new ArgumentException("O nome do cliente não pode ser vazio ou nulo.");
            }

            var clienteId = _clienteRepository.ObterClientePorNome(nomeCliente);

            if (clienteId == null)
            {
                throw new Exception($"Cliente '{nomeCliente}' não encontrado.");
            }
            else
            {
                return clienteId;
            }

            throw new Exception($"Tipo de cliente desconhecido para '{nomeCliente}'.");
        }

        public void BuscarImovelPorInscricaoIPTU()
        {
            Console.Write("Digite a Inscrição IPTU do imóvel: ");
            var inscricaoIptu = Console.ReadLine();
            Imovel imovel = _imovelRepository.BuscarPorInscricaoIPTU(inscricaoIptu);

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
