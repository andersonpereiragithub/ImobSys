namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuDeListas : BaseMenu
    {
        private readonly ConsoleDataLister _consoleDataLister;

        public MenuDeListas(ConsoleDataLister menuListagem)
        {
            _consoleDataLister = menuListagem;
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
                    _consoleDataLister.ListarTodosClientes();
                    break;
                case 2:
                    _consoleDataLister.ExibirListaDeTodosImoveis();
                    break;
                case 3:
                    _consoleDataLister.ExibirListaTodosIPTUs();
                    break;
                case 4:
                    _consoleDataLister.ExibirProprietarioEListarSeusImoveis();
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
