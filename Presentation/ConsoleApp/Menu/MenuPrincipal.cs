using ImobSys.Application.Services;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.ConsoleApp.Menu;

public class MenuPrincipal : BaseMenu
{
    private readonly MenuCadastro _menuCadastro;
    private readonly MenuBusca _menuBusca;
    private readonly MenuListagem _menuListagem;
    private readonly MenuRemocao _menuRemocao;
    private readonly MenuSecundarioListagem _menuSecundarioListagem;
    private readonly IClienteRepository<Cliente> _clienteRepository;
    private readonly IImovelRepository _imovelRepository;
    private readonly ImovelService _movelService;

    public MenuPrincipal(MenuSecundarioListagem menuSecundarioListagem, IClienteRepository<Cliente> clienteRepository, IImovelRepository imovelRepository, ClienteService clienteService, ImovelService imovelService)
    {
        _clienteRepository = clienteRepository;
        _imovelRepository = imovelRepository;
        _menuCadastro = new MenuCadastro(clienteService, imovelService);
        _menuBusca = new MenuBusca(_clienteRepository, _imovelRepository);
        _menuListagem = new MenuListagem(clienteRepository, imovelRepository);
        _menuRemocao = new MenuRemocao(clienteRepository, imovelRepository);
        _menuSecundarioListagem = new MenuSecundarioListagem(clienteRepository, imovelRepository, clienteService, imovelService);
    }

    public void Exibir()
    {
        bool sair = false;
        while (!sair)
        {

            ExibirCabecalho("Menu Principal");
            Console.WriteLine("╔═════════════╦══════════╦═════════════╦════════════╦══════════╗");
            Console.WriteLine("  Cadastro[1]   Busca[2]   Listagen[3]   Remoção[4]   Sair[0]   ");
            Console.WriteLine("╚═════════════╩══════════╩═════════════╩════════════╩══════════╝");
            //Console.WriteLine("1. Cadastro");
            //Console.WriteLine("\u001b[31m2. Busca\u001b[0m");
            //Console.WriteLine("3. Listagens");
            //Console.WriteLine("\u001b[31m4. Remoção\u001b[0m");
            //Console.WriteLine("0. Sair");

            var opcao = SolicitarOpcaoNumerica(0, 4);

            switch (opcao)
            {
                case 1:
                    _menuCadastro.ExibirMenuCadastro();
                    break;
                case 2:
                    //_menuBusca.Exibir();
                    Console.WriteLine("Opção NÃO IMPLEMENTADA!");
                    break;
                case 3:
                    _menuSecundarioListagem.ExibirMenuListagem();
                    break;
                 case 0:
                    sair = true;
                    break;
                default:
                    Console.WriteLine("\nOpção inválida. Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
