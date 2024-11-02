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
                ExibirCabecalho("menu cadastro");
                Console.WriteLine("1. Cadastrar Novo Cliente");
                Console.WriteLine("2. Cadastrar Novo Imóvel");
                Console.WriteLine("0. Voltar");
                Console.WriteLine("========================================");

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
