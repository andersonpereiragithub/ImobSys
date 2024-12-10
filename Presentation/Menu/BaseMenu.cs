using ImobSys.Presentation.Handler;

    namespace ImobSys.Presentation.Menu
{
    public abstract class BaseMenu
    {
        const int larguraLinha = 101;

        protected void ExibirCabecalho(string titulo)
        {
            int posicaoTitulo = (larguraLinha - titulo.Length) / 2;

            string corTitulo = titulo switch
            {
                "MENU PRINCIPAL" => "\u001b[34m=== " + titulo + " ===\u001b[0m",
                "LISTAGENS" => "\u001b[33m=== " + titulo + " ===\u001b[0m",
                "CADASTRO" => "\u001b[32m=== " + titulo + " ===\u001b[0m",
                "DELETAR IMÓVEIS E CLIENTES" => "\u001b[31m=== " + titulo + " ===\u001b[0m",
                _ => titulo
            };

            LinhaSuperior();
            Console.SetCursorPosition(posicaoTitulo, Console.CursorTop);
            Console.WriteLine($"{corTitulo}");
            LinhaInferior();
            
            //MenuStateManager.Instance.AtivarMenu(Console.CursorTop);
        }

        protected void LinhaSuperior()
        {
            Console.WriteLine("╔" + new string('═', larguraLinha) + '╗');
        }
        protected void LinhaInferior()
        {
            Console.WriteLine("╚" + new string('═', larguraLinha) + "╝");
        }

        protected int SolicitarOpcaoNumerica(int min, int max)
        {
            string textOption = "Escolha uma opção: [ ]";
            int x = 2;
            int y = 7;

            int opcao;
            while (true)
            {
                LinhaSuperior();
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"{textOption}");
                x = 22;
                LinhaInferior();
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
