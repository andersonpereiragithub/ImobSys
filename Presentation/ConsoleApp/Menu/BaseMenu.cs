using System;
using System.Runtime.Intrinsics.X86;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public abstract class BaseMenu
    {
        protected void ExibirCabecalho(string titulo)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"                         \u001b[32m{titulo.ToUpper()}\u001b[0m              ");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        }
        protected int SolicitarOpcaoNumerica(int min, int max)
        {
            string textOption = "Escolha uma opção: [ ]";
            int x = 2;
            int y = 7;

            int opcao;
            while (true)
            {

                Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
                Console.WriteLine();
                Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"{textOption}");
                x = 22;
                Console.SetCursorPosition(x, y);

                if (int.TryParse(Console.ReadLine(), out opcao) && opcao >= min && opcao <= max)
                {
                    return opcao;
                }
                Console.WriteLine($"Entrada inválida! Digite um número entre {min} e {max}.");
            }
        }

        protected void ExibirMensagemErro(string mensagem)
        {
            Console.WriteLine($"\n{mensagem}");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
