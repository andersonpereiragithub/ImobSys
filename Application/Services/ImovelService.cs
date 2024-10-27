using System;
using System.Collections.Generic;
using ImobSys.Domain;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Interfaces;

namespace ImobSys.Application.Services
{
    public class ImovelService
    {
        private readonly IImovelRepository _imovelRepository;
        private readonly IClienteRepository _clienteRepository;

        public ImovelService(IImovelRepository imovelRepository, IClienteRepository clienteRepository)
        {
            _imovelRepository = imovelRepository;
            _clienteRepository = clienteRepository;
        }

        public void CadastrarNovoImovel()
        {
            Console.Clear();
            Console.WriteLine("==== Cadastro de Novo Imóvel ===");

            Console.Write("Inscricao IPTU: ");
            string inscricaoIptu = Console.ReadLine(); //incluir validação

            string tipoImovel = "";
            do
            {
                Console.WriteLine("Tipo do Imóvel");
                Console.WriteLine("              (1)Residencial");
                Console.WriteLine("              (2)Comercial");
                Console.WriteLine("              (3)Misto)");
                Console.WriteLine("                   \nOpção: ");
                if (int.TryParse(Console.ReadLine(), out int escolherTipoImovel))
                {
                    switch (escolherTipoImovel)
                    {
                        case 1:
                            tipoImovel = "Residencial";
                            break;
                        case 2:
                            tipoImovel = "Comercial";
                            break;
                        case 3:
                            tipoImovel = "Misto";
                            break;
                        default:
                            Console.WriteLine("Opção Inválida! Escolha 1, 2 ou 3.");
                            continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Entrada inválida! Digite um número (1, 2 ou 3).");
                }
            } while (true);

            Console.Write("Área Útil (m²): ");
            if (!float.TryParse(Console.ReadLine(), out float areaUtil))
            {
                Console.WriteLine("Entrada inválida para área útil. Operação cancelada.");
                return;
            }

            var proprietarios = new List<Guid>();
            bool adicionarMaisProprietarios = true;

            while (adicionarMaisProprietarios)
            {
                Console.Write("O Proprietário já está cadastrado? (S/N): ");
                string proprietarioCadastrado = Console.ReadLine()?.Trim().ToUpper();

                if (proprietarioCadastrado == "S")
                {
                    Console.Write("Informe o nome do proprietário: ");
                    string nomeProprietario = Console.ReadLine();
                    var cliente = _clienteRepository.BuscarPorNomeCliente(nomeProprietario);

                    if (cliente != null)
                    {
                        proprietarios.Add(cliente.Id);
                        Console.WriteLine($"Proprietário [{cliente.Nome}] adicionado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Proprietário não encontrado. Verifique o nome e tente novamente.");
                    }
                }
                else if (proprietarioCadastrado == "N")
                {
                    Console.WriteLine("Cadastrar novo Proprietário:");
                    var clienteService = new ClienteService(_clienteRepository);
                    clienteService.CadastrarNovoCliente();
                }
                else
                {
                    Console.WriteLine("Resposta inválida! Tente novamente...");
                }

                Console.Write("Deseja adicionar outro proprietário? (S/N): ");
                adicionarMaisProprietarios = Console.ReadLine()?.Trim().ToUpper() == "S";
            }

            if (proprietarios.Count == 0)
            {
                Console.WriteLine("É necessário informar ao menos um proprietário. Cadastro cancelado.");
                Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
                Console.ReadKey();
                return;
            }

            var novoImovel = new Imovel
            {
                Id = Guid.NewGuid(),
                InscricaoIPTU = inscricaoIptu,
                TipoImovel = tipoImovel,
                AreaUtil = areaUtil,
                Proprietarios = proprietarios
            };

            _imovelRepository.SalvarImovel(novoImovel);
            Console.WriteLine("Imóvel cadastrado com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para retornar ao Menu.");
            Console.ReadKey();
        }
    }
}
