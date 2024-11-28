using System;

namespace ImobSys.Presentation.Handler
{
    public class UserInteractionHandler
    {
        public int SolicitarOpcaoNumerica(string mensagem, int min, int max)
        {
            while (true)
            {
                ExibirMensagem($"{mensagem} [{min}-{max}]: ", ConsoleColor.Cyan);
                if (int.TryParse(Console.ReadLine(), out int opcao) && opcao >= min && opcao <= max)
                {
                    return opcao;
                }
                ExibirErro($"Entrada inválida! Digite um número entre {min} e {max}.");
            }
        }

        public string SolicitarEntrada(string mensagem, bool permitirVazio = false)
        {
            while (true)
            {
                ExibirMensagem($"{mensagem}: ", ConsoleColor.Cyan);
                var entrada = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(entrada) || permitirVazio)
                {
                    return entrada;
                }
                ExibirErro("Entrada não pode ser vazia. Tente novamente.");
            }
        }

        public bool SolicitarConfirmacao(string mensagem)
        {
            while (true)
            {
                ExibirMensagem($"{mensagem} (S/N): ", ConsoleColor.Cyan);
                var entrada = Console.ReadLine()?.Trim().ToUpper();

                if (entrada == "S") return true;
                if (entrada == "N") return false;

                ExibirErro("Entrada inválida! Digite 'S' para Sim ou 'N' para Não.");
            }
        }

        /// <summary>
        /// Exibe uma mensagem com a cor padrão (branca).
        /// </summary>
        public void ExibirMensagem(string mensagem, ConsoleColor cor = ConsoleColor.White)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

        /// <summary>
        /// Exibe uma mensagem de erro em vermelho.
        /// </summary>
        public void ExibirErro(string mensagem)
        {
            ExibirMensagem($"Erro: {mensagem}", ConsoleColor.Red);
        }

        /// <summary>
        /// Exibe uma mensagem de sucesso em verde.
        /// </summary>
        public void ExibirSucesso(string mensagem)
        {
            ExibirMensagem($"Sucesso: {mensagem}", ConsoleColor.Green);
        }
    }
}
