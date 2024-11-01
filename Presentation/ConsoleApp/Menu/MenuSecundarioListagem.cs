using ImobSys.Application.Services;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.ConsoleApp.Menu;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuSecundarioListagem
    {
        private readonly MenuCadastro _menuCadastro;
        private readonly MenuBusca _menuBusca;
        private readonly MenuListagem _menuListagem;
        private readonly MenuRemocao _menuRemocao;
        private readonly IClienteRepository<Cliente> _clienteRepository;
        private readonly IImovelRepository _imovelRepository;
        private readonly ImovelService _movelService;

        public MenuSecundarioListagem(IClienteRepository<Cliente> clienteRepository, IImovelRepository imovelRepository, ClienteService clienteService, ImovelService imovelService)
        {
            _menuCadastro = new MenuCadastro(clienteService, imovelService);
            _menuBusca = new MenuBusca(_clienteRepository, _imovelRepository);
            _menuListagem = new MenuListagem(clienteRepository, imovelRepository);
            _menuRemocao = new MenuRemocao(clienteRepository, imovelRepository);
        }

        public void ExibirMenuListagem()
        {
            bool sair = false;
            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("=========== MENU PRINCIPAL ===========");
                Console.WriteLine("Cadastro");
                Console.WriteLine("Busca");
                Console.WriteLine("\u001b[33mListagens\u001b[0m");
                Console.WriteLine("             1. Lista de Clientes");
                Console.WriteLine("             2. Lista de Imóveis");
                Console.WriteLine("             3. Lista de IPTUs");
                Console.WriteLine("\u001b[31m4. Remoção\u001b[0m");
                Console.WriteLine("0. Voltar");
                Console.WriteLine("======================================");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        _menuListagem.ListarTodosClientes();
                        //_menuCadastro.ExibirMenuCadastro();
                        break;
                    case "2":
                        _menuListagem.ListarTodosImoveis();
                        break;
                    case "3":
                        _menuListagem.ListarTodosIPTUs();
                        break;
                    case "4":

                        break;

                    case "0":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
