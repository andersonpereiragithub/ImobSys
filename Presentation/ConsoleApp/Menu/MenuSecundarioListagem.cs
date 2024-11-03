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
        private readonly ImovelService _imovelService;

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
                Console.Clear();
                string tituloAmarelo = "LISTAGENS";
                ExibirCabecalho(tituloAmarelo);
                Console.WriteLine("╔═════════════╦════════════╦══════════╦════════════╦═════════╗");
                Console.WriteLine("  Clientes[1]   Imóveis[2]   IPTUs[3]   Remoção[4]  \u001b[31mVoltar[0]\u001b[0m");
                Console.WriteLine("╚═════════════╩════════════╩══════════╩════════════╩═════════╝");

                var opcao = SolicitarOpcaoNumerica(0, 4);

                switch (opcao)
                {
                    case 1:
                        _menuListagem.ListarTodosClientes();
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
