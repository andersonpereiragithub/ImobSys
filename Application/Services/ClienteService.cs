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
            Console.WriteLine("==== Cadastro de Novo Cliente ====");

            string tipoCliente = _userInteractionHandler.SolicitarEntrada("Cliente (1)Pessoa Física / (2)Pessoa Jurídica? ", true);

            Cliente novoCliente;

            if (tipoCliente == "1")
            {
                novoCliente = CadastrarPessoaFisica();
            }
            else if (tipoCliente == "2")
            {
                novoCliente = CadastrarPessoaJuridica();
            }
            else
            {
                Console.WriteLine("Tipode de cliente inválido. Cadastro cancelado.");
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
            string cpf = _userInteractionHandler.SolicitarEntrada("CPF: ", true);
            string endereco = _userInteractionHandler.SolicitarEntrada("Endereço (opcional): ");


            string telefone = _userInteractionHandler.SolicitarEntrada("Telefone (opcional): ");

            Console.Write("Tipo de Relação (1) Locador / (2) Locatário / (3) Fiador: ");
            List<TiposRelacao> tipoRelacoes = ObterTiposRelacoes();

            return new PessoaFisica(nome, cpf, new Endereco { Logradouro = endereco }, tipoRelacoes)
            {
                Telefone = telefone
            };
        }

        private PessoaJuridica CadastrarPessoaJuridica()
        {
            string razaoSocial = _userInteractionHandler.SolicitarEntrada("Razão Social: ", true);
            string cnpj = _userInteractionHandler.SolicitarEntrada("CNPJ: ", true);
            string nomeRepresentante = _userInteractionHandler.SolicitarEntrada("Nome do Representante (opcional): ");
            string inscricaoEstadual = _userInteractionHandler.SolicitarEntrada("Inscrição Estadual (opcional): ");
            string endereco = _userInteractionHandler.SolicitarEntrada("Endereço (opcional): ");
            string telefone = _userInteractionHandler.SolicitarEntrada("Telefone (opcional): ");

            Console.Write("Tipo de Relação (1) Locador / (2) Locatário / (3) Fiador: ");
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
                Console.WriteLine("Escolha o Tipo de Relação:");
                Console.WriteLine("1. Locador");
                Console.WriteLine("2. Locatário");
                Console.WriteLine("3. Fiador");
                Console.Write("Opção: ");

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

        public void RemoverCliente()
        {
            try
            {
                Console.SetCursorPosition(2, 7);
                var nomeCliente = _userInteractionHandler.SolicitarEntrada("Inserir o Nome do Cliente para Excluir:", true);

                var clienteId = _clienteRepository.ObterClientePorNome(nomeCliente);

                var sucesso = _clienteRepository.RemoverCliente(clienteId);

                if (sucesso)
                {
                    _userInteractionHandler.ExibirSucesso($"Cliente '{nomeCliente}' removido com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(2, 7);
                _userInteractionHandler.ExibirErro($"Erro: {ex.Message} Operação Cancelada.");

                Console.SetCursorPosition(2, 9);
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu...");
                Console.ReadKey();
            }
        }
    }
}
