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

        public void BuscarClientePorNome()
        {
            try
            {
                Console.Write("Digite o nome do cliente: ");
                var nome = Console.ReadLine();
                if (nome != null)
                {
                    object cliente = _clienteRepository.BuscarPorNomeCliente(nome);

                    if (cliente != null)
                    {
                        Console.WriteLine($"Cliente encontrado: {nome}");

                        if (cliente is PessoaFisica pessoaFisica)
                        {
                            Console.WriteLine($"CPF: {pessoaFisica.CPF} ");
                        }
                        else if (cliente is PessoaJuridica pessoaJuridica)
                        {
                            Console.WriteLine($"CNPJ: {pessoaJuridica.CNPJ}");
                            if (!string.IsNullOrWhiteSpace(pessoaJuridica.NomeRepresentante))
                            {
                                Console.WriteLine($"Nome do Representante: {pessoaJuridica.NomeRepresentante}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cliente não encontrado");
                    }
                }
                else
                {
                    Console.WriteLine("Nome não pode ser vazio.");
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
