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
                ExibirMensagem($"{mensagem} ", ConsoleColor.Cyan);
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

        public void ExibirMensagem(string mensagem, ConsoleColor cor = ConsoleColor.White)
        {
            int linhaParaExibir = MenuStateManager.Instance.ObterProximaLinha();
            if (MenuStateManager.Instance.MenuAtivo == true)
            {
                Console.SetCursorPosition(2, linhaParaExibir - 5);
                Console.ForegroundColor = cor;
                Console.Write(mensagem);
                Console.ResetColor();
            }
            else
            {
                Console.SetCursorPosition(2, linhaParaExibir);
                Console.ForegroundColor = cor;
                Console.Write(mensagem);
                Console.ResetColor();
            }

        }

        public void ExibirErro(string mensagem)
        {
            ExibirMensagem($"Erro: {mensagem}", ConsoleColor.Red);
        }

        public void ExibirSucesso(string mensagem)
        {
            ExibirMensagem($"Sucesso: {mensagem}", ConsoleColor.Green);
        }

        public void ExibirMensagemRetornoMenu(string mensagem)
        {
            int linhaParaExibir = MenuStateManager.Instance.ObterProximaLinha();
            Console.SetCursorPosition(2, linhaParaExibir);
            Console.WriteLine($"{mensagem}", ConsoleColor.Green);
        }
    }
}
