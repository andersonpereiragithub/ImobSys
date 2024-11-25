using System;

namespace ImobSys.Presentation.ConsoleApp.Handler
{
    public class OutputHandler
    {
        public void ExibirMensagem(string mensagem)
        {
            Console.Write(mensagem);
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
