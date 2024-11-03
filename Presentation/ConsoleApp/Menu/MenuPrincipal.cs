using ImobSys.Application.Services;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.ConsoleApp.Menu;

public class MenuPrincipal : BaseMenu
{
    private readonly MenuCadastro _menuCadastro;
    private readonly MenuBusca _menuBusca;
    private readonly ConsoleDataLister _menuListagem;
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
        _menuListagem = new ConsoleDataLister(clienteRepository, imovelRepository);
        _menuRemocao = new MenuRemocao(clienteRepository, imovelRepository);
        _menuSecundarioListagem = new MenuSecundarioListagem(clienteRepository, imovelRepository, clienteService, imovelService);
    }

    public void Exibir()
    {
        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            ExibirCabecalho("MENU PRINCIPAL");
            Console.WriteLine("╔═════════════╦══════════╦═════════════╦════════════╦════════╗");
            Console.WriteLine("  Cadastro[1]   Busca[2]   Listagen[3]   Remoção[4]  \u001b[31m Sair[0]\u001b[0m   ");
            Console.WriteLine("╚═════════════╩══════════╩═════════════╩════════════╩════════╝");

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
