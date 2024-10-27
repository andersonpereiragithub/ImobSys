using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.ConsoleApp.Menu;
using ImobSys.Application.Services;

public class MenuPrincipal
{
    private readonly MenuCadastro _menuCadastro;
    private readonly MenuBusca _menuBusca;
    private readonly MenuListagem _menuListagem;
    private readonly MenuRemocao _menuRemocao;
    private readonly ClienteService _clienteService;
    private readonly ImovelService _movelService;

    public MenuPrincipal(IClienteRepository clienteRepository, IImovelRepository imovelRepository, ClienteService clienteService, ImovelService imovelService)
    {
        _menuCadastro = new MenuCadastro(clienteRepository, imovelRepository, clienteService, imovelService);
        _menuBusca = new MenuBusca(clienteRepository, imovelRepository);
        _menuListagem = new MenuListagem(clienteRepository, imovelRepository);
        _menuRemocao = new MenuRemocao(clienteRepository, imovelRepository);
    }

    public void Exibir()
    {
        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("=========== MENU PRINCIPAL ===========");
            Console.WriteLine("1. Cadastro");
            Console.WriteLine("2. Busca");
            Console.WriteLine("3. Listagem");
            Console.WriteLine("4. Remoção");
            Console.WriteLine("0. Sair");
            Console.WriteLine("======================================");
            Console.Write("Escolha uma opção: ");

            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    _menuCadastro.ExibirMenuCadastro();
                    break;
                case "2":
                    //_menuBusca.Exibir();
                    break;
                case "3":
                    _menuListagem.ListarTodosClientes();
                    break;
                case "4":
                    // _menuRemocao.Exibir();
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
