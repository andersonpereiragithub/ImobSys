namespace ImobSys.Application.Ajuda
{
    public class AjudaEntradaDeDados
    {
        public static string SolicitarEntrada(string mensagem, bool obrigatorio = false)
        {
            string entrada;
            do
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine();

                if (obrigatorio && string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Este campo é obrigatório. Por favor, insira um valor.");
                }
                else
                {
                    return string.IsNullOrWhiteSpace(entrada) ? null : entrada;
                }
            } while (obrigatorio);

            return entrada;
        }
    }
}
