using System;

namespace ImobSys.Presentation.ConsoleApp.Handler
{
    public class InputHandler
    {
        public string SolicitarTexto(string mensagem)
        {
            Console.Write($"{mensagem}: ");
            return Console.ReadLine()?.Trim();
        }

        public int SolicitarNumero(string mensagem)
        {
            while (true)
            {
                Console.Write($"{mensagem}: ");
                if (int.TryParse(Console.ReadLine(), out var numero))
                {
                    return numero;
                }
                Console.WriteLine("Entrada inválida. Por favor, insira um número válido.");
            }
        }

        public int SolicitarOpcaoNumerica(int minimo, int maximo)
        {
            while (true)
            {
                Console.Write("Escolha uma opção: ");
                if (int.TryParse(Console.ReadLine(), out int opcao) && opcao >= minimo && opcao <= maximo)
                {
                    return opcao;
                }

                Console.WriteLine($"Por favor, insira um número entre {minimo} e {maximo}.");
            }
        }

        public bool SolicitarConfirmacao(string mensagem)
        {
            Console.Write($"{mensagem} (S/N): ");
            var resposta = Console.ReadLine()?.Trim().ToUpper();
            return resposta == "S";
        }

        public string SolicitarEntrada(string mensagem, bool obrigatorio = false,
                                              Func<string, bool> validacaoExtra = null,
                                              string mensagemErroValidacao = "Entrada inválida. Por favor, tente novamente.")
        {
            string entrada;
            do
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine()?.Trim();

                if (obrigatorio && string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Este campo é obrigatório. Por favor, insira um valor.");
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(entrada) && validacaoExtra != null && !validacaoExtra(entrada))
                {
                    Console.WriteLine(mensagemErroValidacao);
                    continue;
                }

                return string.IsNullOrWhiteSpace(entrada) ? null : entrada;
            } while (true);
        }
    }
}
