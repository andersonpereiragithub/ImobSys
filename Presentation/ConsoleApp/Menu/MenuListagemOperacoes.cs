using ImobSys.Application.Services;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuListagemOperacoes : BaseMenu
    {
        private readonly ConsoleDataLister _menuListagem;

        public MenuListagemOperacoes(ConsoleDataLister menuListagem)
        {
            _menuListagem = menuListagem;
        }

        private void ExibirOpcoes()
        {
            Console.WriteLine("╔══════════════════════╦═════════════════════╦═══════════════════╦═════════════════════╦══════════════╗");
            Console.WriteLine("      Clientes[1]            Imóveis[2]             IPTUs[3]         Prop. Imóveis[4]      \u001b[31mVoltar[0]\u001b[0m");
            Console.WriteLine("╚══════════════════════╩═════════════════════╩═══════════════════╩═════════════════════╩══════════════╝");
        }
        public void ProcessarOpcao(int opcao, ref bool sair)
        {
            switch (opcao)
            {
                case 1:
                    _menuListagem.ListarTodosClientes();
                    break;
                case 2:
                    _menuListagem.ExibirListaDeTodosImoveis();
                    break;
                case 3:
                    _menuListagem.ExibirListaTodosIPTUs();
                    break;
                case 4:
                    _menuListagem.ExibirProprietarioEListarSeusImoveis();
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
