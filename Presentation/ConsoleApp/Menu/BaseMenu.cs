using System;
using System.Runtime.Intrinsics.X86;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public abstract class BaseMenu
    {
        protected void ExibirCabecalho(string titulo)
        {
            string corTitulo = titulo switch
            {
                "MENU PRINCIPAL" => "\u001b[34m" + titulo + "\u001b[0m",
                "LISTAGENS" => "\u001b[33m" + titulo + "\u001b[0m",
                "CADASTRO" => "\u001b[32m" + titulo + "\u001b[0m",
                _ => titulo
            };

            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"                         {corTitulo}              ");
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
