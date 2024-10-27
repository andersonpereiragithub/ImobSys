using System;
using ImobSys.Application.Services;
using ImobSys.Domain.Interfaces;
using ImobSys.Infrastructure.Repositories;
using ImobSys.Presentation.ConsoleApp.Menu;

namespace ImobSys.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IImovelRepository imovelRepository = new JsonImovelRepository("imoveis.json");
            IClienteRepository clienteRepository = new JsonClienteRepository(imovelRepository, "clientes.json");
            ClienteService clienteService = new ClienteService(clienteRepository); //DÚVIDA SOBRE ISSO!
            
            var menuPrincipal = new MenuPrincipal(clienteRepository, imovelRepository, clienteService);

            menuPrincipal.Exibir();
        }

        //    private static JsonClienteRepository _clienteRepository;
        //    private static JsonImovelRepository _imovelRepository;
        //    static void Main(string[] args)
        //    {
        //        _clienteRepository = new JsonClienteRepository(new MockImovelRepository(), "clientes.json");
        //        _imovelRepository = new JsonImovelRepository("imoveis.json");

        //        bool sair = false;

        //        while (!sair)
        //        {
        //            Console.Clear();
        //            Console.WriteLine("=========== MENU PRINCIPAL ===========");
        //            Console.WriteLine("1. Cadastrar Novo Cliente");
        //            Console.WriteLine("2. Cadastrar Novo Imóvel");
        //            Console.WriteLine("3. Listar Todos os Clientes");
        //            Console.WriteLine("4. Listar Todos os Imóveis");
        //            Console.WriteLine("5. Buscar Cliente por ID");
        //            Console.WriteLine("6. Buscar Imóvel por ID");
        //            Console.WriteLine("7. Remover Cliente");
        //            Console.WriteLine("8. Remover Imóvel");
        //            Console.WriteLine("0. Sair");
        //            Console.WriteLine("======================================");
        //            Console.Write("Escolha uma opção: ");

        //            var opcao = Console.ReadLine();

        //            switch (opcao)
        //            {
        //                case "1":
        //                    CadastrarNovoCliente();
        //                    break;
        //                case "2":
        //                    CadastrarNovoImovel();
        //                    break;
        //                case "3":
        //                    ListarTodosClientes();
        //                    break;
        //                case "4":
        //                    ListarTodosImoveis();
        //                    break;
        //                case "5":
        //                    BuscarClientePorId();
        //                    break;
        //                case "6":
        //                    BuscarImovelPorId();
        //                    break;
        //                case "7":
        //                    RemoverCliente();
        //                    break;
        //                case "8":
        //                    RemoverImovel();
        //                    break;
        //                case "0":
        //                    sair = true;
        //                    break;
        //                default:
        //                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente.");
        //                    Console.ReadKey();
        //                    break;
        //            }
        //        }
        //    }

        //    // Funções esboço para cada operação do menu
        //    static void CadastrarNovoCliente()
        //    { /* Implementação futura */

        //        Console.Clear();
        //        Console.WriteLine("==== Cadastro de Novo Clinte ====");

        //        Console.Write("Nome: ");
        //        string nome = Console.ReadLine();

        //        Console.Write("CPF: ");
        //        string cpf = Console.ReadLine();

        //        Console.Write("Tipo do Cliente (Proprietário/Locatário): ");
        //        string tipoCliente = Console.ReadLine();

        //        var novoCliente = new Cliente
        //        {
        //            Id = Guid.NewGuid(),
        //            Nome = nome,
        //            CPF = cpf,
        //            TipoCliente = tipoCliente
        //        };

        //        _clienteRepository.SalvarCliente(novoCliente);
        //        Console.WriteLine("Cliente cadastrado com Sucesso!");
        //        Console.WriteLine("Pressione qualquer tecla para retornar ao meunu...");
        //        Console.ReadKey();
        //    }

        //    static void CadastrarNovoImovel()
        //    { /* Implementação futura */

        //        Console.Clear();
        //        Console.WriteLine("==== Cadastro de Novo Imóvel ===");

        //        Console.Write("Inscricao IPTU: ");
        //        string inscricaoIptu = Console.ReadLine();

        //        string tipoImovel = "";
        //        do
        //        {
        //            Console.Write("Tipo do Imóvel (1)Residencial/(2)Comercial/(3)Misto)" +
        //                "\nOpção: ");

        //            int escolherTipoImovel;

        //            if (int.TryParse(Console.ReadLine(), out escolherTipoImovel))
        //            {
        //                switch (escolherTipoImovel)
        //                {
        //                    case 1:
        //                        tipoImovel = "Residencial";
        //                        break;
        //                    case 2:
        //                        tipoImovel = "Comercial";
        //                        break;
        //                    case 3:
        //                        tipoImovel = "Misto";
        //                        break;
        //                    default:
        //                        Console.WriteLine("Opção Inválida! Escolha 1, 2 ou 3.");
        //                        continue;
        //                }
        //                break; // Sai do loop se uma opção válida foi escolhida
        //            }
        //            else
        //            {
        //                Console.WriteLine("Entrada inválida! Digite um número (1, 2 ou 3).");
        //            }
        //        }
        //        while (true);

        //        Console.Write("Área Útil (m²): ");
        //        float areaUtil = float.Parse(Console.ReadLine());

        //        var proprietarios = new List<Guid>();
        //        bool adicionarMaisProprietarios = true;

        //        while (adicionarMaisProprietarios)
        //        {
        //            Console.Write("O Proprietário já está cadastrado? (S/N): ");
        //            string proprietarioCadastrado = Console.ReadLine()?.Trim().ToUpper();

        //            if (proprietarioCadastrado == "S")
        //            {
        //                Console.Write("Informe o nome do proprietário: ");
        //                string nomeProprietario = Console.ReadLine();
        //                var cliente = _clienteRepository.BuscarPorNomeCliente(nomeProprietario);

        //                if (cliente != null)
        //                {
        //                    proprietarios.Add(cliente.Id);
        //                    Console.WriteLine($"Proprietário [{cliente.Nome}] adicionado com sucesso!");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Proprietário não encontrado. Verifique o nome e tente novamente.");
        //                }
        //            }
        //            else if (proprietarioCadastrado == "N")
        //            {
        //                Console.WriteLine("Cadastrar novo Proprietário: ");
        //                CadastrarNovoCliente();
        //            }
        //            else
        //            {
        //                Console.WriteLine("Resposta inválida! Tente novamente...");
        //            }

        //            Console.WriteLine("Desja adicioar outro proprietário? (S/N): ");
        //            adicionarMaisProprietarios = Console.ReadLine()?.Trim().ToUpper() == "S";
        //        }

        //        if (proprietarios.Count == 0)
        //        {
        //            Console.WriteLine("É necessário informar ao menos um proprietário. Cadastro cancelado.");
        //            Console.WriteLine("Pressione qualquer tecla para retornar ao menu...");
        //            Console.ReadKey();
        //            return;
        //        }

        //        var novoImovel = new Imovel
        //        {
        //            Id = Guid.NewGuid(),
        //            InscricaoIPTU = inscricaoIptu,
        //            TipoImovel = tipoImovel,
        //            AreaUtil = areaUtil,
        //            Proprietarios = proprietarios
        //        };

        //        _imovelRepository.SalvarImovel(novoImovel);
        //        Console.WriteLine("Imóvel cadastrado com sucesso!");
        //        Console.WriteLine("Pressione qualquer teclar para retornar ao Menu.");
        //        Console.ReadKey();
        //    }

        //    static void ListarTodosClientes() { /* Implementação futura */ }
        //    static void ListarTodosImoveis() { /* Implementação futura */ }
        //    static void BuscarClientePorId() { /* Implementação futura */ }
        //    static void BuscarImovelPorId() { /* Implementação futura */ }
        //    static void RemoverCliente() { /* Implementação futura */ }
        //    static void RemoverImovel() { /* Implementação futura */ }
        //}
        //public class MockImovelRepository : IImovelRepository
        //{
        //    public Imovel BuscarPorIdImovel(Guid id)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public bool ClientePossuiImovel(Guid clienteId) => false;

        //    public List<Imovel> ListarTodosImovel()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void RemoverImovel(Guid id)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void SalvarImovel(Imovel imovel)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}


