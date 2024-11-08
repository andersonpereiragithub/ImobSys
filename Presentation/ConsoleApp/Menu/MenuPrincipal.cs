using ImobSys.Application.Services;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.ConsoleApp.Menu;

public class MenuPrincipal : BaseMenu
{
    private readonly MenuListagemOperacoes _menuSecundarioListagem;
    private readonly IClienteRepository<Cliente> _clienteRepository;
    private readonly IImovelRepository _imovelRepository;
    private readonly IClienteService _clienteService;
    private readonly IImovelService _imovelService;
    private readonly MenuCadastro _menuCadastro;
    private readonly MenuRemocao _menuRemocao;

    public MenuPrincipal(
        MenuListagemOperacoes menuSecundarioListagem,
        IClienteRepository<Cliente> clienteRepository,
        IImovelRepository imovelRepository,
        IClienteService clienteService,
        IImovelService imovelService,
        MenuCadastro menuCadastro,
        MenuRemocao menuRemocao)
    {
        _menuSecundarioListagem = menuSecundarioListagem;
        _clienteRepository = clienteRepository;
        _imovelRepository = imovelRepository;
        _clienteService = clienteService;
        _imovelService = imovelService;
        _menuCadastro = menuCadastro;   
        _menuRemocao = menuRemocao;
    }

    public void Exibir()
    {
        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            ExibirCabecalho("MENU PRINCIPAL");
            Console.WriteLine("╔═════════════════╦══════════════╦═════════════════╦════════════════╦════════════════╦════════════════╗");
            Console.WriteLine("    Cadastro[1]       Busca[2]       Listagen[3]       Editar[4]        Deletar[5]        \u001b[31m Sair[0]\u001b[0m   ");
            Console.WriteLine("╚═════════════════╩══════════════╩═════════════════╩════════════════╩════════════════╩════════════════╝");

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
                case 4:
                    Console.WriteLine("NÃO IMPLEMENTADO!");
                    break;
                case 5:
                    _menuRemocao.ExibirMenuDeRemocao();
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
