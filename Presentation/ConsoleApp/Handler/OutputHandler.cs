using System;

namespace ImobSys.Presentation.ConsoleApp.Handlers
{
    public class OutputHandler
    {
        public void ExibirMensagem(string mensagem)
        {
            Console.WriteLine(mensagem);
        }

        public void ExibirErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

        public void ExibirSucesso(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }
    }
}
