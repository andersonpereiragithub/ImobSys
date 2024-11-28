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
                    var nomeCliente = _userInteractionHandler.SolicitarEntrada("Digite o nome do cliente");
                    try
                    {
                        _clienteService.RemoverCliente(nomeCliente);
                        _userInteractionHandler.ExibirSucesso($"Cliente [{nomeCliente}] removido com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        _userInteractionHandler.ExibirErro(ex.Message);
                    }
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

        //public void RemoverCliente()
        //{
        //    BoxDePesquisa("Digite o NOME:");
        //    var nome = Console.ReadLine();
        //    var clienteId = _clienteRepository.ObterClientePorNome(nome);
        //    var cliente = _clienteRepository.BuscarPorIdCliente(clienteId);

        //    if (cliente == null)
        //    {
        //        LimparLinha(11);
        //        ExibirMensagem($"[{nome}] - \u001b[31mCLIENTE NÃO ENCONTRADO!\u001b[0m", 2, 11);
        //    }

        //    if (cliente is PessoaFisica pf)
        //    {
        //        ConfirmarERemoverCliente(pf.Nome, pf.Id);
        //    }
        //    else if (cliente is PessoaJuridica pj)
        //    {
        //        ConfirmarERemoverCliente(pj.RazaoSocial, pj.Id);
        //    }
            
        //    ExibirMensagem("Pressione qualquer tecla para continuar...", 2, 14);
        //    Console.ReadKey();
        //}

        private void ExibirMensagem(string mensagem, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(mensagem);
        }

        //private void ConfirmarERemoverCliente(string nomeCliente, Guid clienteId)
        //{
        //    ExibirMensagem($"Cliente encontrado. \u001b[31mDeseja Excluir? (S/N)\u001b[0m [ ]", 2, 9);
        //    Console.SetCursorPosition(45, 9);

        //    if (SolicitarConfirmacao())
        //    {
        //        _clienteRepository.RemoverCliente(clienteId);
        //        ExibirMensagem($"\u001b[31m{nomeCliente} Cliente removido com sucesso!\u001b[0m", 2, 11);
        //    }
        //    else
        //    {
        //        LimparLinha(11);
        //        ExibirMensagem("\u001b[32mOperação Cancelada.\u001b[0m", 6, 11);
        //    }
        //}

        private bool SolicitarConfirmacao()
        {
            return Console.ReadLine()?.Trim().ToUpper() == "S";
        }

        private void LimparLinha(int y)
        {
            Console.SetCursorPosition(2, y);
            Console.WriteLine(new string(' ', Console.WindowWidth - 2));
        }

        //public void RemoverImovel()
        //{
        //    BoxDePesquisa("Digite o ENDEREÇO: ");
        //    var inscricaoIptu = Console.ReadLine();
        //    var imovel = _imovelService. .BuscarPorInscricaoIPTU(inscricaoIptu);

        //    if (imovel == null)
        //    {
        //        Console.WriteLine("Imóvel não encontrado.");
        //    }
        //    else
        //    {
        //        _imovelRepository.RemoverImovel(imovel.Id);
        //        Console.WriteLine("Imóvel removido com sucesso.");
        //    }
        //    Console.WriteLine("Pressione qualquer tecla para continuar...");
        //    Console.ReadKey();
        //}

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
