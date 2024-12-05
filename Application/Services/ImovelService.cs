using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Enums;
using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.Handler;

namespace ImobSys.Application.Services
{
    public class ImovelService : IImovelService
    {
        private readonly UserInteractionHandler _userInteractionHandler;
        private readonly IImovelRepository _imovelRepository;
        private readonly IClienteService _clienteService;

        public ImovelService(UserInteractionHandler interactionHandler, IImovelRepository imovelRepository, IClienteService clienteService)
        {
            _userInteractionHandler = interactionHandler;
            _imovelRepository = imovelRepository;
            _clienteService = clienteService;
        }

        public void CadastrarNovoImovel()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 2);
            _userInteractionHandler.ExibirMensagem("==== Cadastro de Novo Imóvel ====");

            var novoImovel = new Imovel { Id = Guid.NewGuid() };

            novoImovel.TipoImovel = _userInteractionHandler.ConfigurarTipoImovel();
            novoImovel.AreaUtil = _userInteractionHandler.ObterAreaUtil();

            novoImovel.InscricaoIPTU = _userInteractionHandler.SolicitarCampo("Inscrição de IPTU:", false);
            novoImovel.DetalhesTipoImovel = _userInteractionHandler.ObterTipoImovel();

            _userInteractionHandler.ConfigurarLocacaoEVenda(novoImovel);

            novoImovel.Endereco = _userInteractionHandler.ObterEndereco();

            _userInteractionHandler.ConfigurarCaracteristicasInternas(novoImovel);

            AtribuirProprietarios(novoImovel);

            _imovelRepository.SalvarImovel(novoImovel);
            Console.Clear();
            _userInteractionHandler.ExibirSucesso("Imóvel cadastrado com sucesso!");

            _userInteractionHandler.ExibirMensagem("Pressione qualquer tecla para retornar ao Menu.");
            Console.ReadKey();
        }

        private void AtribuirProprietarios(Imovel imovel)
        {
            bool respostaAfirmativa = _userInteractionHandler.LerOpcaoSimNao("O Proprietário já está cadastrado? (S/N): ");

            var proprietarios = new List<Guid>();
            bool adicionarMaisProprietarios = true;

            while (adicionarMaisProprietarios)
            {
                if (respostaAfirmativa)
                {
                    try
                    {
                        string nomeProprietario = _userInteractionHandler.SolicitarEntrada("Informe o nome do proprietário: ", true);

                        var idCliente = _clienteService.ObterClientePorNome(nomeProprietario);
                        var proprietario = _clienteService.BuscarPorClienteId(idCliente);

                        if (proprietario != null)
                        {
                            if (proprietario is PessoaFisica pf)
                            {
                                if (!pf.ImoveisId.Contains(imovel.Id))
                                {
                                    pf.ImoveisId.Add(imovel.Id);
                                }
                                proprietarios.Add(pf.Id);
                                _clienteService.SalvarCliente(pf);
                                Console.WriteLine($"Proprietário [{pf.Nome}] adicionado com sucesso!");
                            }
                            else if (proprietario is PessoaJuridica pj)
                            {
                                if (!pj.ImoveisId.Contains(imovel.Id))
                                {
                                    pj.ImoveisId.Add(imovel.Id);
                                }
                                proprietarios.Add(pj.Id);
                                _clienteService.SalvarCliente(pj);
                                Console.WriteLine($"Proprietário [{pj.RazaoSocial}] adicionado com sucesso!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro: {ex.Message}");
                    }

                }
                else if (!respostaAfirmativa)
                {
                    Console.WriteLine("Cadastrar novo Proprietário:");
                    var clienteService = _clienteService.CadastrarNovoCliente;
                    
                    string clienteNovoCadastrado = _userInteractionHandler.SolicitarEntrada("Digite Nome do Cliente Cadastrado: ", true);

                    var clienteId = _clienteService.ObterClientePorNome(clienteNovoCadastrado);
                    var cliente = _clienteService.BuscarPorClienteId(clienteId);

                    if (cliente != null)
                    {
                        if (cliente is PessoaFisica pf)
                        {
                            pf.ImoveisId.Add(imovel.Id);
                            proprietarios.Add(pf.Id);
                            _clienteService.SalvarCliente(pf);
                        }
                        else if (cliente is PessoaJuridica pj)
                        {
                            pj.ImoveisId.Add(imovel.Id);
                            proprietarios.Add(pj.Id);
                            _clienteService.SalvarCliente(pj);
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
    }
}
