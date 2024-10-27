using System;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuCadastro
    {
        public void Exibir()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=========== MENU DE CADASTRO ===========");
                Console.WriteLine("1. Cadastrar Novo Cliente");
                Console.WriteLine("2. Cadastrar Novo Imóvel");
                Console.WriteLine("0. Voltar");
                Console.WriteLine("========================================");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarNovoCliente();
                        break;
                    case "2":
                        CadastrarNovoImovel();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CadastrarNovoCliente()
        {
            // Implementação ou chamada para cadastrar cliente
            Console.WriteLine("Função de cadastro de cliente...");
            Console.ReadKey();
        }

        private void CadastrarNovoImovel()
        {
            // Implementação ou chamada para cadastrar imóvel
            Console.WriteLine("Função de cadastro de imóvel...");
            Console.ReadKey();
        }
    }
}
