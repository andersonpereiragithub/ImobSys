using ImobSys.Domain.Entities.Clientes;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain.Interfaces;
using System;
using ImobSys.Presentation.ConsoleApp.Handler;
using ImobSys.Presentation.Handler;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuCadastro : BaseMenu
    {
        private readonly IClienteService _clienteService;
        private readonly IImovelService _imovelService;
        private readonly UserInteractionHandler _userInteractionHandler;

        public MenuCadastro(IClienteService clienteService, IImovelService imovelService, UserInteractionHandler userInteractionHandler)
        {   
            _clienteService = clienteService;
            _imovelService = imovelService;
            _userInteractionHandler = userInteractionHandler;
        }

        public void ExibirMenuCadastro()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();
                ExibirCabecalho("CADASTRO");
                Console.WriteLine("╔══════════════════╦═════════════════╦════════════════╦═══════════════╦═══════════════════════════════╗");
                Console.WriteLine("    Clientes[1]        Imóveis[2]         Editar[3]       Deletar[4]            \u001b[31m Voltar[0]\u001b[0m   ");
                Console.WriteLine("╚══════════════════╩═════════════════╩════════════════╩═══════════════╩═══════════════════════════════╝");

                var opcao = SolicitarOpcaoNumerica(0, 4);

                switch (opcao)
                {
                    case 1:
                        _clienteService.CadastrarNovoCliente();
                        break;
                    case 2:
                        _imovelService.CadastrarNovoImovel();
                        break;
                    case 4:
                        _clienteService.RemoverCliente();
                        break;
                    case 0:
                        voltar = true;
                        break;
                    default:
                        _userInteractionHandler.ExibirErro("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
