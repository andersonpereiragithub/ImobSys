using ImobSys.Domain.Entities.Clientes;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain.Interfaces;
using System;
using ImobSys.Presentation.Handler;

namespace ImobSys.Presentation.Menu
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
                ExibirOpcoesMenu();

                var opcao = SolicitarOpcaoNumerica(0, 5);

                try
                {
                    ProcessarOpcao(opcao, ref voltar);
                }
                catch (Exception ex)
                {
                    _userInteractionHandler.ExibirErro($"Erro: {ex.Message}");
                }
            }
        }

        private void ProcessarOpcao(int opcao, ref bool voltar)
        {
            switch (opcao)
            {
                case 1:
                    _clienteService.CadastrarNovoCliente();
                    break;
                case 2:
                    _imovelService.CadastrarNovoImovel();
                    break;
                case 3:
                    //_clienteService.AlterarClientel();
                    break;
                case 4:
                    _clienteService.RemoverImovelDeCliente();
                    break;
                case 5:
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

        private void ExibirOpcoesMenu()
        {
            Console.WriteLine("╔═════════════════════════════╦═══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("  [\u001b[31m1\u001b[0m]Clientes   [\u001b[31m2\u001b[0m]Imóveis          [\u001b[31m3\u001b[0m]Alterar Cliente   [\u001b[31m4\u001b[0m]Deletar Imóveis   [\u001b[31m5\u001b[0m]Deletar   \u001b[31m[0]Voltar\u001b[0m   ");
            Console.WriteLine("╚═════════════════════════════╩═══════════════════════════════════════════════════════════════════════╝");
        }
    }
}
