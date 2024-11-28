using System;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.ConsoleApp.Handler;
using ImobSys.Presentation.Handler;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuRemocao : BaseMenu
    {
        private readonly IClienteService _clienteService;
        private readonly IImovelService _imovelService;
        private readonly UserInteractionHandler _userInteractionHandler;

        public MenuRemocao(IClienteService clienteService, IImovelService imovelService, UserInteractionHandler userInteractionHandler)
        {
            _clienteService = clienteService;
            _imovelService = imovelService;
            _userInteractionHandler = userInteractionHandler;
        }

        private void ExibirOpcoesRemocao()
        {
            Console.WriteLine("╔══════════════════════╦════════════════════╦════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("   Buscar Clientes[1]    Buscar Imóveis[2]       \u001b[31mVoltar[0]\u001b[0m");
            Console.WriteLine("╚══════════════════════╩════════════════════╩════════════════════════════════════════════════════════════════════════════╝");
        }

        public void ProcessarOpcoesDeRemocao(int opcao, ref bool sair)
        {
            switch (opcao)
            {
                case 1:
                    _clienteService.RemoverCliente();
                    break;
                case 2:

                    break;
                case 0:
                    sair = true;
                    break;
                default:
                    _userInteractionHandler.ExibirMensagem("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                    break;
            }
        }

        public void ExibirMenuDeRemocao()
        {

            bool sair = false;
            while (!sair)
            {
                Console.Clear();
                ExibirCabecalho("DELETAR IMÓVEIS E CLIENTES");
                ExibirOpcoesRemocao();

                var opcao = SolicitarOpcaoNumerica(0, 2);
                ProcessarOpcoesDeRemocao(opcao, ref sair);
            }
        }

        public void BoxDePesquisa(string textoBusca)
        {
            int x = 2;
            int y = 9;

            Console.SetCursorPosition(x, y);
            Console.WriteLine($"\u001b[31m{textoBusca}\u001b[0m");
            Console.WriteLine("╔═════════════════════════════════════════════════════════════╗");
            Console.WriteLine();
            Console.WriteLine("╚═════════════════════════════════════════════════════════════╝");
            Console.SetCursorPosition(x, 11);
        }
    }
}
