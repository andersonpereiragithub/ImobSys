using ImobSys.Domain.Entities.Clientes;
using ImobSys.Application.Services;
using ImobSys.Domain.Interfaces;
using System;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuCadastro : BaseMenu
    {
        private readonly ClienteService _clienteService;
        private readonly ImovelService _imovelService;

        public MenuCadastro( ClienteService clienteService, ImovelService imovelService)
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
                ExibirCabecalho("Menu Cadastro");
                Console.WriteLine("╔═════════════════════════╦═══════════════════════╦══════════╗");
                Console.WriteLine("        Clientes[1]               Imóveis[2]       \u001b[31m Voltar[0]\u001b[0m   ");
                Console.WriteLine("╚═════════════════════════╩═══════════════════════╩══════════╝");

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
