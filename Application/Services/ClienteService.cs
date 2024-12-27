using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Enums;
using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.Handler;

namespace ImobSys.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository<Cliente> _clienteRepository;
        private readonly IImovelRepository _imovelRepository;
        private readonly UserInteractionHandler _userInteractionHandler;


        public ClienteService(IClienteRepository<Cliente> clienteRepository, IImovelRepository imovelRepository, UserInteractionHandler userInteractionHandler)
        {
            _clienteRepository = clienteRepository;
            _imovelRepository = imovelRepository;
            _userInteractionHandler = userInteractionHandler;
        }

        public (object cliente, List<Imovel> imoveis) ObterClienteESeusImoveis(string nomeCliente)
        {
            var imoveis = new List<Imovel>();

            var clienteId = _clienteRepository.ObterClientePorNome(nomeCliente);
            var isEmpty = clienteId == Guid.Empty;

            if (isEmpty)
            {
                throw new Exception($"Cliente com nome '{nomeCliente}' não encontrado.");
            }

            imoveis = _imovelRepository.ObterImoveisPorCliente(clienteId);

            return (clienteId, imoveis);
        }

        public void CadastrarNovoCliente()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 2);
            _userInteractionHandler.ExibirMensagem("==== Cadastro de Novo Cliente ====");

            var tipoCliente = _userInteractionHandler.LerIntPositivo("\nCliente (1)Pessoa Física / (2)Pessoa Jurídica? ");

            Cliente novoCliente;

            if (tipoCliente == 1)
            {
                novoCliente = CadastrarPessoaFisica();
            }
            else if (tipoCliente == 2)
            {
                novoCliente = CadastrarPessoaJuridica();
            }
            else
            {
                _userInteractionHandler.ExibirErro("Tipode de cliente inválido. Cadastro cancelado.");
                return;
            }

            _clienteRepository.SalvarCliente(novoCliente);
            _userInteractionHandler.ExibirSucesso("Cliente cadastrado com sucesso!");

            Console.WriteLine("\nPressione qualquer tecla para retornar ao menu...");
            Console.ReadKey();
        }

        public List<Cliente> ListarTodosClientes()
        {
            return _clienteRepository.ListarTodosClientes();
        }

        private PessoaFisica CadastrarPessoaFisica()
        {
            string nome = _userInteractionHandler.SolicitarEntrada("Nome: ", true);
            string cpf = _userInteractionHandler.SolicitarEntrada($"{nome} seu CPF: ", true);
            string endereco = _userInteractionHandler.SolicitarEntrada("Endereço (opcional): ");
            
            string telefone = _userInteractionHandler.SolicitarEntrada("Telefone (opcional): ");

            Console.Write($" {nome} é: (1) Locador / (2) Locatário / (3) Fiador: ");
            List<TiposRelacao> tipoRelacoes = ObterTiposRelacoes();

            return new PessoaFisica(nome, cpf, new Endereco { Logradouro = endereco }, tipoRelacoes)
            {
                Telefone = telefone
            };
        }

        private PessoaJuridica CadastrarPessoaJuridica()
        {
            string razaoSocial = _userInteractionHandler.SolicitarEntrada("Razão Social: ", true);
            string cnpj = _userInteractionHandler.SolicitarEntrada($"{razaoSocial} seu CNPJ: ", true);
            string nomeRepresentante = _userInteractionHandler.SolicitarEntrada("Nome do Representante (opcional): ");
            string inscricaoEstadual = _userInteractionHandler.SolicitarEntrada("Inscrição Estadual (opcional): ");
            string endereco = _userInteractionHandler.SolicitarEntrada("Endereço (opcional): ");
            string telefone = _userInteractionHandler.SolicitarEntrada("Telefone (opcional): ");

            Console.Write($"{razaoSocial} é: (1) Locador / (2) Locatário / (3) Fiador: ");
            List<TiposRelacao> tiposRelacoes = ObterTiposRelacoes();
            return new PessoaJuridica(razaoSocial, cnpj, new Endereco { Logradouro = endereco }, tiposRelacoes, nomeRepresentante, inscricaoEstadual)
            {
                Telefone = telefone
            };
        }

        private List<TiposRelacao> ObterTiposRelacoes()
        {
            var tipoRelacoes = new List<TiposRelacao>();

            bool adicionarMais;
            do
            {
                if (Enum.TryParse(Console.ReadLine(), out TiposRelacao tipoRelacao))
                {
                    tipoRelacoes.Add(tipoRelacao);
                    Console.Write("Deseja adicionar outra relação? (S/N): ");
                    adicionarMais = Console.ReadLine()?.Trim().ToUpper() == "S";
                }
                else
                {
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    adicionarMais = true;
                }
            } while (adicionarMais);

            return tipoRelacoes;
        }

        public void AtribuirProprietarios(Imovel imovel)
        {
            var proprietarios = new List<Guid>();
            bool adicionarMaisProprietarios = true;

            while (adicionarMaisProprietarios)
            {
                bool respostaAfirmativa = _userInteractionHandler.LerOpcaoSimNao("O Proprietário já está cadastrado? (S/N): ");

                if (respostaAfirmativa)
                {
                    try
                    {
                        string nomeProprietario = _userInteractionHandler.SolicitarEntrada("Informe o nome do proprietário: ", true);

                        var idCliente = ObterClientePorNome(nomeProprietario);
                        var proprietario = BuscarClientePorId(idCliente);

                        if (proprietario != null)
                        {
                            if (!proprietario.ImoveisId.Contains(imovel.Id))
                            {
                                proprietario.ImoveisId.Add(imovel.Id);
                            }
                            proprietarios.Add(proprietario.Id);
                            SalvarCliente(proprietario);
                            _userInteractionHandler.ExibirSucesso($"Proprietário [{(proprietario is PessoaFisica ?
                                               ((PessoaFisica)proprietario).Nome :
                                               ((PessoaJuridica)proprietario).RazaoSocial)}] adicionado com sucesso!");
                        }
                    }
                    catch (Exception ex)
                    {
                        _userInteractionHandler.ExibirErro($"Erro: {ex.Message}");
                    }
                }
                else if (!respostaAfirmativa)
                {
                    CadastrarNovoCliente();

                    string clienteNovoCadastrado = _userInteractionHandler.SolicitarEntrada("Confirme o Nome do Cliente cadastrado: ", true);

                    var idCliente = ObterClientePorNome(clienteNovoCadastrado);
                    var proprietario = BuscarClientePorId(idCliente);

                    if (proprietario != null)
                    {
                        proprietario.ImoveisId.Add(imovel.Id);
                        proprietarios.Add(proprietario.Id);
                        SalvarCliente(proprietario);
                    }
                }
                else
                {
                    _userInteractionHandler.ExibirErro("Resposta inválida! Tente novamente...");
                }

                bool adicionarOutro = _userInteractionHandler.LerOpcaoSimNao("Deseja adicionar outro proprietário? (S/N): ");
                if (!adicionarOutro)
                {
                    adicionarMaisProprietarios = false;
                }
            }
            imovel.Proprietarios = proprietarios;
        }

        public void RemoverCliente()
        {
            try
            {
                var nomeCliente = _userInteractionHandler.SolicitarEntrada("Digite o Nome para Excluir:", true);

                var clienteId = _clienteRepository.ObterClientePorNome(nomeCliente);

                var imoveis = _imovelRepository.ObterImoveisPorCliente(clienteId);

                if (imoveis != null && imoveis.Count > 0)
                {
                    _userInteractionHandler.ExibirErro($"Cliente '{nomeCliente}' possui imóveis e não pode ser removido.");
                    _userInteractionHandler.ExibirMensagemRetornoMenu("\nPressione qualquer tecla para retornar ao menu...");
                }
                else
                {
                    _clienteRepository.RemoverCliente(clienteId);
                    _userInteractionHandler.ExibirSucesso($"Cliente '{nomeCliente}' excluido com sucesso!");
                    _userInteractionHandler.ExibirMensagemRetornoMenu("\nPressione qualquer tecla para retornar ao menu...");
                }
            }
            catch (Exception ex)
            {
                _userInteractionHandler.ExibirErro(ex.Message);

                _userInteractionHandler.ExibirMensagemRetornoMenu("\nPressione qualquer tecla para retornar ao menu...");
                Console.ReadKey();
            }
        }

        public Guid ObterClientePorNome(string nomeProprietario)
        {
            return _clienteRepository.ObterClientePorNome(nomeProprietario);
        }

        public Cliente BuscarClientePorId(Guid id)
        {
            return _clienteRepository.BuscarClientePorId(id);
        }

        public void SalvarCliente(Cliente cliente)
        {
            _clienteRepository.SalvarCliente(cliente);
        }

        private bool RemoverImovelDoCliente(Guid clienteId, Guid ImovelId)
        {
            var cliente = _clienteRepository.BuscarClientePorId(clienteId);

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado!");
            }

            if (!cliente.ImoveisId.Contains(ImovelId))
            {
                throw new Exception("O cliente não possui este imóvel!");
            }

            cliente.ImoveisId.Remove(ImovelId);
            _clienteRepository.SalvarCliente(cliente);

            return true;
        }

        public void RemoverImovelDeCliente()
        {
            var clienteNome = _userInteractionHandler.SolicitarEntrada("Digite o nome do cliente: ");
            var clienteId = ObterClientePorNome(clienteNome);

            var imovelIdStr = _userInteractionHandler.SolicitarEntrada("Digite o ID do imóvel: ");
            if (Guid.TryParse(imovelIdStr, out Guid imovelId))
            {
                try
                {
                    RemoverImovelDoCliente(clienteId, imovelId);
                    _userInteractionHandler.ExibirSucesso("Imóvel removido do cliente com sucesso!");
                }
                catch (Exception ex)
                {
                    _userInteractionHandler.ExibirErro(ex.Message);
                }
            }
            else
            {
                _userInteractionHandler.ExibirErro("ID do imóvel inválido.");
            }
        }
    }
}
