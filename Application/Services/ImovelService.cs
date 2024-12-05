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
        private readonly IClienteRepository<Cliente> _clienteRepository;

        public ImovelService(UserInteractionHandler interactionHandler, IImovelRepository imovelRepository, IClienteRepository<Cliente> clienteRepository)
        {
            _userInteractionHandler = interactionHandler;
            _imovelRepository = imovelRepository;
            _clienteRepository = clienteRepository;
        }

        public void CadastrarNovoImovel()
        {
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
            var proprietarios = new List<Guid>();
            bool adicionarMaisProprietarios = true;

            while (adicionarMaisProprietarios)
            {
                Console.Write("O Proprietário já está cadastrado? (S/N): ");
                string resposta = Console.ReadLine()?.Trim().ToUpper();

                if (resposta == "S")
                {
                    try
                    {
                        string nomeProprietario = _userInteractionHandler.SolicitarEntrada("Informe o nome do proprietário: ", true);

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
                                _clienteRepository.SalvarCliente(pf);
                                Console.WriteLine($"Proprietário [{pf.Nome}] adicionado com sucesso!");
                            }
                            else if (proprietario is PessoaJuridica pj)
                            {
                                if (!pj.ImoveisId.Contains(imovel.Id))
                                {
                                    pj.ImoveisId.Add(imovel.Id);
                                }
                                proprietarios.Add(pj.Id);
                                _clienteRepository.SalvarCliente(pj);
                                Console.WriteLine($"Proprietário [{pj.RazaoSocial}] adicionado com sucesso!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro: {ex.Message}");
                    }

                }
                else if (resposta == "N")
                {
                    Console.WriteLine("Cadastrar novo Proprietário:");
                    var clienteService = new ClienteService(_clienteRepository, _imovelRepository, _userInteractionHandler);
                    clienteService.CadastrarNovoCliente();

                    string clienteNovoCadastrado = _userInteractionHandler.SolicitarEntrada("Digite do Cliente Cadastrado: ", true);

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
    }
}
