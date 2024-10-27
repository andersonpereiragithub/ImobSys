using System;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuOperacoes
    {
        public void Exibir()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=========== MENU DE OPERAÇÕES ===========");
                Console.WriteLine("1. Buscar Cliente por ID");
                Console.WriteLine("2. Buscar Imóvel por ID");
                Console.WriteLine("3. Listar Todos os Clientes");
                Console.WriteLine("4. Listar Todos os Imóveis");
                Console.WriteLine("5. Remover Cliente");
                Console.WriteLine("6. Remover Imóvel");
                Console.WriteLine("0. Voltar");
                Console.WriteLine("=========================================");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        BuscarClientePorId();
                        break;
                    case "2":
                        BuscarImovelPorId();
                        break;
                    case "3":
                        ListarTodosClientes();
                        break;
                    case "4":
                        ListarTodosImoveis();
                        break;
                    case "5":
                        RemoverCliente();
                        break;
                    case "6":
                        RemoverImovel();
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

        private void BuscarClientePorId() { /* Implementação */ }
        private void BuscarImovelPorId() { /* Implementação */ }
        private void ListarTodosClientes() { /* Implementação */ }
        private void ListarTodosImoveis() { /* Implementação */ }
        private void RemoverCliente() { /* Implementação */ }
        private void RemoverImovel() { /* Implementação */ }
    }
}
