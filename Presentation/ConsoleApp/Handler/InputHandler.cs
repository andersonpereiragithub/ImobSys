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

        public bool SolicitarConfirmacao(string mensagem)
        {
            Console.Write($"{mensagem} (S/N): ");
            var resposta = Console.ReadLine()?.Trim().ToUpper();
            return resposta == "S";
        }
    }
}
