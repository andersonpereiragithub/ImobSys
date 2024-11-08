using ImobSys.Domain.Entities.Clientes;
using ImobSys.Application.Services.Interfaces;
using System.Collections.Generic;
using ImobSys.Domain.Interfaces;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Enums;
using ImobSys.Domain;
using System;

namespace ImobSys.Application.Services
{
    public class ImovelService : IImovelService
    {
        private readonly IImovelRepository _imovelRepository;
        private readonly IClienteRepository<Cliente> _clienteRepository;

        public ImovelService(IImovelRepository imovelRepository, IClienteRepository<Cliente> clienteRepository)
        {
            _imovelRepository = imovelRepository;
            _clienteRepository = clienteRepository;
        }

        public void CadastrarNovoImovel()
        {
            Console.Clear();
            Console.WriteLine("==== Cadastro de Novo Imóvel ===");

            Console.Write("Inscricao IPTU: ");
            string inscricaoIptu = Console.ReadLine();

            string tipoImovel = "";
            do
            {
                Console.WriteLine("Tipo do Imóvel");
                Console.WriteLine("              (1)Residencial");
                Console.WriteLine("              (2)Comercial");
                Console.WriteLine("              (3)Misto)");
                Console.Write("                     Opção: ");
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

            Console.Write("Detalhes do Tipo de Imóvel (Casa, Apto, Loja, etc.): ");
            string detalhesTipoImovel = Console.ReadLine();

            // ======================= Dados de locação e venda =======================

            Console.Clear();
            Console.Write("O imóvel está disponível para locação? (S/N): ");
            bool paraLocacao = Console.ReadLine()?.Trim().ToUpper() == "S";

            string statusLocacao = "Disponível";
            decimal? valorAluguel = null;
            if (paraLocacao)
            {
                Console.Write("Status de Locação (1) Disponível / (2) Alugado: ");
                if (int.TryParse(Console.ReadLine(), out int opcaoStatusLocacao) && (opcaoStatusLocacao == 1 || opcaoStatusLocacao == 2))
                {
                    statusLocacao = opcaoStatusLocacao == 1 ? "Disponível" : "Alugado";
                }

                Console.Write("Valor do Aluguel (se disponível): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal aluguel))
                {
                    valorAluguel = aluguel;
                }
            }

            Console.Write("O imóvel está disponível para venda? (S/N): ");
            bool paraVenda = Console.ReadLine()?.Trim().ToUpper() == "S";

            decimal? valorVenda = null;
            if (paraVenda)
            {
                Console.Write("Valor de Venda: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal venda))
                {
                    valorVenda = venda;
                }
            }


            // ================ Coletando dados do endereço ================

            Console.Clear();
            Console.WriteLine("==== Informações de Endereço ====");

            string tipoLogradouro = SolicitarTipoLogradouro().ToString();

            Console.Write("Logradouro: ");
            string logradouro = Console.ReadLine();

            Console.Write("Número: ");
            string numero = Console.ReadLine();

            Console.Write("Complemento (ex.: Casa 1, Apto 301): ");
            string complemento = Console.ReadLine();

            Console.Write("Bairro: ");
            string bairro = Console.ReadLine();

            Console.Write("Cidade (Localidade): ");
            string cidade = Console.ReadLine();

            Console.Write("UF: ");
            string uf = Console.ReadLine();

            Console.Write("CEP: ");
            string cep = Console.ReadLine();

            // Criando o objeto de endereço
            var endereco = new Endereco
            {
                TipoLogradouro = tipoLogradouro,
                Logradouro = logradouro,
                Numero = numero,
                Complemento = complemento,
                Bairro = bairro,
                Cidade = cidade,
                UF = uf,
                CEP = cep
            };

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
                        if (cliente is PessoaFisica pessoaFisica)
                        {
                            proprietarios.Add(pessoaFisica.Id);
                            Console.WriteLine($"Proprietário [{pessoaFisica.Nome}] adicionado com sucesso!");
                        }
                        else if(cliente is PessoaJuridica pessoaJuridica)
                        {
                            proprietarios.Add(pessoaJuridica.Id);
                            Console.WriteLine($"Proprietário [{pessoaJuridica.RazaoSocial}] adicionado com sucesso!");
                        }
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

            // ================== Coleta de Detalhes Internos do Imóvel ==================

            Console.Clear();
            Console.WriteLine("==== Detalhes Internos do Imóvel ====");

            Console.Write("Número de Quartos: ");
            int quartos = LerIntPositivo();

            Console.Write("Número de Salas: ");
            int salas = LerIntPositivo();

            Console.Write("Número de Banheiros: ");
            int banheiros = LerIntPositivo();

            Console.Write("Número de Garagens: ");
            int garagens = LerIntPositivo();

            Console.Write("Possui Cozinha? (S/N): ");
            bool cozinha = LerSimNao();

            Console.Write("Possui Copa? (S/N): ");
            bool copa = LerSimNao();

            Console.Write("Possui Quintal? (S/N): ");
            bool quintal = LerSimNao();

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
                DetalhesTipoImovel = detalhesTipoImovel,
                AreaUtil = areaUtil,
                Endereco = endereco,
                Proprietarios = proprietarios,
                ParaLocacao = paraLocacao,
                StatusLocacao = statusLocacao,
                ValorAluguel = valorAluguel,
                ParaVenda = paraVenda,
                ValorVenda = valorVenda,
                Quartos = quartos,
                Salas = salas,
                Banheiros = banheiros,
                Garagens = garagens,
                Cozinha = cozinha,
                Copa = copa,
                Quintal = quintal
            };

            _imovelRepository.SalvarImovel(novoImovel);
            Console.WriteLine("Imóvel cadastrado com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para retornar ao Menu.");
            Console.ReadKey();
        }

        public List<Imovel> ListarTodosImoveis()
        {
            return _imovelRepository.ListarTodosImovel();
        }

        public TipoLogradouro SolicitarTipoLogradouro()
        {
            Console.WriteLine("Selecione o Tipo de Logradouro:");

            // Exibe todas as opções do enum com seus índices
            foreach (var tipo in Enum.GetValues(typeof(TipoLogradouro)))
            {
                Console.WriteLine($" [{(int)tipo}] {tipo}");
            }

            Console.Write("Escolha o tipo de Logradouro: ");
            // Lê a escolha do usuário
            int escolha;
            while (!int.TryParse(Console.ReadLine(), out escolha) || !Enum.IsDefined(typeof(TipoLogradouro), escolha))
            {
                Console.WriteLine("Opção inválida! Digite o número correspondente a uma opção da lista.");
            }

            return (TipoLogradouro)escolha;
        }

        private int LerIntPositivo()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int numero) && numero >= 0)
                    return numero;

                Console.Write("Entrada inválida! Digite um número positivo: ");
            }
        }

        private bool LerSimNao()
        {
            while (true)
            {
                string entrada = Console.ReadLine()?.Trim().ToUpper();
                if (entrada == "S") return true;
                if (entrada == "N") return false;

                Console.Write("Entrada inválida! Digite 'S' para Sim ou 'N' para Não: ");
            }
        }
    }
}
