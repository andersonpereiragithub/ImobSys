using System;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuPrincipal
    {
        private readonly MenuCadastro _menuCadastro;
        private readonly MenuOperacoes _menuOperacoes;

        public MenuPrincipal()
        {
            _menuCadastro = new MenuCadastro();
            _menuOperacoes = new MenuOperacoes();
        }

        public void Exibir()
        {
            bool sair = false;

            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("=========== MENU PRINCIPAL ===========");
                Console.WriteLine("1. Cadastros");
                Console.WriteLine("2. Operações");
                Console.WriteLine("0. Sair");
                Console.WriteLine("======================================");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        _menuCadastro.Exibir();
                        break;
                    case "2":
                        _menuOperacoes.Exibir();
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
