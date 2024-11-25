using ImobSys.Domain.Entities.Clientes;
using ImobSys.Application.Services.Interfaces;
using System.Collections.Generic;
using ImobSys.Domain.Interfaces;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Enums;
using ImobSys.Domain;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ImobSys.Presentation.ConsoleApp.Handler;

namespace ImobSys.Application.Services
{
    public class ImovelService : IImovelService
    {
        private readonly InputHandler _inputHandler;
        private readonly OutputHandler _outputHandler;
        private readonly IImovelRepository _imovelRepository;
        private readonly IClienteRepository<Cliente> _clienteRepository;

        public ImovelService(InputHandler inputHandler, OutputHandler outputHandler, IImovelRepository imovelRepository, IClienteRepository<Cliente> clienteRepository)
        {
            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
            _imovelRepository = imovelRepository;
            _clienteRepository = clienteRepository;
        }

        public void CadastrarNovoImovel()
        {
            Console.Clear();
            var novoImovel = new Imovel { Id = Guid.NewGuid() };

            novoImovel.TipoImovel = ConfigurarTipoImovel();
            novoImovel.AreaUtil = ObterAreaUtil();
            novoImovel.DetalhesTipoImovel = ObterTipoImovel();

            ConfigurarLocacaoEVenda(novoImovel);

            novoImovel.Endereco = ObterEndereco();

            ConfigurarCaracteristicasInternas(novoImovel);

            AtribuirProprietarios(novoImovel);

            _imovelRepository.SalvarImovel(novoImovel);
            _outputHandler.ExibirSucesso("Imóvel cadastrado com sucesso!");
            _outputHandler.ExibirMensagem("Pressione qualquer tecla para retornar ao Menu.");
            Console.ReadKey();
        }

        private string ConfigurarTipoImovel()
        {
            string tipoImovel = "";
            do
            {
                _outputHandler.ExibirMensagem("Tipo do Imóvel \n    (1)Residencial\n    (2)Comercial\n    (3)Misto)\n          Opção: ");
                if (int.TryParse(Console.ReadLine(), out int escolha))
                {
                    tipoImovel = escolha switch
                    {
                        1 => "Residencial",
                        2 => "Comercial",
                        3 => "Misto",
                        _ => tipoImovel
                    };
                    if (!string.IsNullOrEmpty(tipoImovel)) break;
                }
                Console.WriteLine("Opção Inválida! Escolha 1, 2 ou 3.");
            } while (true);
            return tipoImovel;
        }

        private float ObterAreaUtil()
        {
            float areaUtil;

            Console.Write("Área Útil (m²): ");
            while (!float.TryParse(Console.ReadLine(), out areaUtil))
            {
                Console.Write("Entrada inválida para área útil. Digite novamente: ");
            }
            return areaUtil;
        }

        private string ObterTipoImovel()
        {
            Console.WriteLine("Selecione o Tipo de Imvel:");
            var listaDeSubTiposImoveis = Enum.GetValues(typeof(SubtipoImovel));

            foreach (var tipo in listaDeSubTiposImoveis)
            {
                Console.WriteLine($" [{(int)tipo}] {tipo}");
            }

            return _inputHandler.SolicitarEntrada("Tipo de Imóvel:", true);
        }

        private void ConfigurarLocacaoEVenda(Imovel imovel)
        {
            Console.Write("O imóvel está disponível para locação? (S/N): ");
            imovel.ParaLocacao = Console.ReadLine()?.Trim().ToUpper() == "S";
            if (imovel.ParaLocacao)
            {
                Console.Write("Status de Locação (1) Disponível / (2) Alugado: ");
                imovel.StatusLocacao = int.TryParse(Console.ReadLine(), out int status) && status == 2 ? "Alugado" : "Disponível";

                Console.Write("Valor do Aluguel: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal aluguel))
                {
                    imovel.ValorAluguel = aluguel;
                }
            }

            Console.Write("O imóvel está disponível para venda? (S/N): ");
            imovel.ParaVenda = Console.ReadLine()?.Trim().ToUpper() == "S";
            if (imovel.ParaVenda)
            {
                Console.Write("Valor de Venda: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal venda))
                {
                    imovel.ValorVenda = venda;
                }
            }
        }

        private Endereco ObterEndereco()
        {
            Console.Clear();
            Console.WriteLine("==== Informações de Endereço ====");
            var endereco = new Endereco();

            endereco.TipoLogradouro = SolicitarTipoLogradouro().ToString();

            Console.Write($"Logradouro: {endereco.TipoLogradouro} ");
            endereco.Logradouro = Console.ReadLine();

            Console.Write($"{endereco.TipoLogradouro} {endereco.Logradouro}, Número:  ");
            endereco.Numero = Console.ReadLine();

            Console.Write($"{endereco.TipoLogradouro} {endereco.Logradouro}, {endereco.Numero} Complemento (ex.: Casa 1, Apto 301): ");
            endereco.Complemento = Console.ReadLine();

            Console.Write("Bairro: ");
            endereco.Bairro = Console.ReadLine();

            //Console.Write("Cidade (Localidade): ");
            endereco.Cidade = "Juiz de Fora";

            //Console.Write("UF: ");
            endereco.UF = "MG";

            Console.Write("CEP: ");
            endereco.CEP = Console.ReadLine();

            return endereco;
        }

        private void ConfigurarCaracteristicasInternas(Imovel imovel)
        {
            Console.Write("Número de Quartos: ");
            imovel.Quartos = LerIntPositivo();

            Console.Write("Número de Salas: ");
            imovel.Salas = LerIntPositivo();

            Console.Write("Número de Banheiros: ");
            imovel.Banheiros = LerIntPositivo();

            Console.Write("Número de Garagens: ");
            imovel.Garagens = LerIntPositivo();

            Console.Write("Possui Cozinha? (S/N): ");
            imovel.Cozinha = LerSimNao();

            Console.Write("Possui Copa? (S/N): ");
            imovel.Copa = LerSimNao();

            Console.Write("Possui Quintal? (S/N): ");
            imovel.Quintal = LerSimNao();
        }

        private void AtribuirProprietarios(Imovel imovel)
        {
            var proprietarios = new List<Guid>();
            bool adicionarMaisProprietarios = true;

            while (adicionarMaisProprietarios)
            {
                Console.Write("O Proprietário já está cadastrado? (S/N): ");
                string resposta = Console.ReadLine()?.Trim().ToUpper();

                if (resposta == "S")
                {
                    string nomeProprietario = _inputHandler.SolicitarEntrada("Informe o nome do proprietário: ", true);

                    var cliente = _clienteRepository.ObterClientePorNome(nomeProprietario);
                    var proprietario = _clienteRepository.BuscarPorIdCliente(cliente);

                    if (proprietario != null)
                    {
                        if (proprietario is PessoaFisica pf)
                        {
                            if (!pf.ImoveisId.Contains(imovel.Id))
                            {
                                pf.ImoveisId.Add(imovel.Id);
                            }
                            proprietarios.Add(pf.Id);
                    //        _clienteRepository.SalvarCliente(pf);
                            Console.WriteLine($"Proprietário [{pf.Nome}] adicionado com sucesso!");
                        }
                        else if (proprietario is PessoaJuridica pj)
                        {
                            if (!pj.ImoveisId.Contains(imovel.Id))
                            {
                                pj.ImoveisId.Add(imovel.Id);
                            }
                            proprietarios.Add(pj.Id);
                            //_clienteRepository.SalvarCliente(pj);
                            Console.WriteLine($"Proprietário [{pj.RazaoSocial}] adicionado com sucesso!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Proprietário não encontrado. Verifique o nome e tente novamente.");
                    }
                }
                else if (resposta == "N")
                {
                    Console.WriteLine("Cadastrar novo Proprietário:");
                    var clienteService = new ClienteService(_clienteRepository, _imovelRepository, _outputHandler, _inputHandler);
                    clienteService.CadastrarNovoCliente();
                    
                    string clienteNovoCadastrado = _inputHandler.SolicitarEntrada("Digite do Cliente Cadastrado: ", true);

                    var clienteId = _clienteRepository.ObterClientePorNome(clienteNovoCadastrado);
                    var cliente = _clienteRepository.BuscarPorIdCliente(clienteId);

                    if (cliente != null)
                    {
                        if (cliente is PessoaFisica pf)
                        {
                            pf.ImoveisId.Add(imovel.Id);
                            proprietarios.Add(pf.Id);
                            _clienteRepository.SalvarCliente(pf);
                        }
                        else if (cliente is PessoaJuridica pj)
                        {
                            pj.ImoveisId.Add(imovel.Id);
                            proprietarios.Add(pj.Id);
                            _clienteRepository.SalvarCliente(pj);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Resposta inválida! Tente novamente...");
                }

                Console.Write("Deseja adicionar outro proprietário? (S/N): ");
                adicionarMaisProprietarios = Console.ReadLine()?.Trim().ToUpper() == "S";
            }

            imovel.Proprietarios = proprietarios;
        }

        public List<Imovel> ListarTodosImoveis()
        {
            return _imovelRepository.ListarTodosImovel();
        }

        public TipoLogradouro SolicitarTipoLogradouro()
        {
            Console.WriteLine("Selecione o Tipo de Logradouro:");

            foreach (var tipo in Enum.GetValues(typeof(TipoLogradouro)))
            {
                Console.WriteLine($" [{(int)tipo}] {tipo}");
            }

            Console.Write("Escolha o tipo de Logradouro: ");

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
