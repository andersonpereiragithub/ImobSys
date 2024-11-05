using ImobSys.Application.Services;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuListagemOperacoes : BaseMenu
    {
        private readonly MenuCadastro _menuCadastro;
        private readonly MenuBusca _menuBusca;
        private readonly ConsoleDataLister _menuListagem;
        private readonly MenuRemocao _menuRemocao;
        private readonly IClienteRepository<Cliente> _clienteRepository;
        private readonly IImovelRepository _imovelRepository;
        private readonly ImovelService _imovelService;

        public MenuListagemOperacoes(MenuCadastro menuCadastro, MenuBusca menuBusca, ConsoleDataLister menuListagem, MenuRemocao menuRemocao)
        {
            _menuCadastro = menuCadastro;
            _menuBusca = menuBusca;
            _menuListagem = menuListagem;
            _menuRemocao = menuRemocao;
        }

        private void ExibirOpcoes()
        {
            Console.WriteLine("╔═════════════╦════════════╦══════════╦════════════╦═════════╗");
            Console.WriteLine("  Clientes[1]   Imóveis[2]   IPTUs[3]   Remoção[4]  \u001b[31mVoltar[0]\u001b[0m");
            Console.WriteLine("╚═════════════╩════════════╩══════════╩════════════╩═════════╝");
        }
        public void ProcessarOpcao(int opcao, ref bool sair)
        {
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
                    _menuRemocao.RemoverCliente();
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

        public void ExibirMenuListagem()
        {
            bool sair = false;
            while (!sair)
            {
                Console.Clear();
                ExibirCabecalho("LISTAGENS");
                ExibirOpcoes();

                var opcao = SolicitarOpcaoNumerica(0, 4);
                ProcessarOpcao(opcao, ref sair);
            }
        }
    }
}
