using ImobSys.Domain.Entities.Clientes;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain.Interfaces;
using System;
using ImobSys.Application.Ajuda;

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
                        var cliente = AjudaEntradaDeDados.SolicitarEntrada("Inserir o Nome do Cliente para Excluir:", true);
                        var clientes = _clienteService.ListarTodosClientes();

                        foreach (var c in clientes)
                        {
                            if (c.Nome == cliente && c is PessoaFisica pf && pf.Nome == cliente ||
                               c is PessoaJuridica pj && pj.RazaoSocial == cliente)
                            {
                                _clienteService.RemoverCliente(c.Id);
                            }
                        }
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
