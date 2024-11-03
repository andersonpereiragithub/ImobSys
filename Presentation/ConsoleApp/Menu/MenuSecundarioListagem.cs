using ImobSys.Application.Services;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.ConsoleApp.Menu;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuSecundarioListagem : BaseMenu
    {
        private readonly MenuCadastro _menuCadastro;
        private readonly MenuBusca _menuBusca;
        private readonly ConsoleDataLister _menuListagem;
        private readonly MenuRemocao _menuRemocao;
        private readonly IClienteRepository<Cliente> _clienteRepository;
        private readonly IImovelRepository _imovelRepository;
        private readonly ImovelService _movelService;

        public MenuSecundarioListagem(IClienteRepository<Cliente> clienteRepository, IImovelRepository imovelRepository, ClienteService clienteService, ImovelService imovelService)
        {
            _menuCadastro = new MenuCadastro(clienteService, imovelService);
            _menuBusca = new MenuBusca(_clienteRepository, _imovelRepository);
            _menuListagem = new ConsoleDataLister(clienteRepository, imovelRepository);
            _menuRemocao = new MenuRemocao(clienteRepository, imovelRepository);
        }

        public void ExibirMenuListagem()
        {
            bool sair = false;
            while (!sair)
            {
                //ExibirCabecalho("\u001b[31mListagem\u001b[0m");
                //Console.WriteLine("   \u001b[33mListagens\u001b[0m");
                //Console.WriteLine("             1. Lista de Clientes");
                //Console.WriteLine("             2. Lista de Imóveis");
                //Console.WriteLine("             3. Lista de IPTUs");
                //Console.WriteLine("\u001b[31m4. Remoção\u001b[0m");
                //Console.WriteLine("0. Voltar");

                Console.Clear();
                ExibirCabecalho("\u001b[31mListagem\u001b[0m");
                Console.WriteLine("╔═════════════╦════════════╦══════════╦════════════╦═════════╗");
                Console.WriteLine("  Clientes[1]   Imóveis[2]   IPTUs[3]   Remoção[4]  \u001b[31m Voltar[0]\u001b[0m   ");
                Console.WriteLine("╚═════════════╩════════════╩══════════╩════════════╩═════════╝");

                var opcao = SolicitarOpcaoNumerica(0, 4);

                switch (opcao)
                {
                    case 1:
                        _menuListagem.ListarTodosClientes();
                        //_menuCadastro.ExibirMenuCadastro();
                        break;
                    case 2:
                        _menuListagem.ListarTodosImoveis();
                        break;
                    case 3:
                        _menuListagem.ListarTodosIPTUs();
                        break;
                    case 4:
                        Console.WriteLine("Opção NÃO IMPLEMENTADA!");
                        break;
                    case 0:
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
