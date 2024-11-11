using ImobSys.Domain.Entities.Clientes;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain.Interfaces;
using System;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuCadastro : BaseMenu
    {
        private readonly IClienteService _clienteService;
        private readonly IImovelService _imovelService;

        public MenuCadastro(IClienteService clienteService, IImovelService imovelService)
        {
            _clienteService = clienteService;
            _imovelService = imovelService;
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

                var opcao = SolicitarOpcaoNumerica(0, 2);

                switch (opcao)
                {
                    case 1:
                        _clienteService.CadastrarNovoCliente();
                        break;
                    case 2:
                        _imovelService.CadastrarNovoImovel();
                        break;
                    case 0:
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
