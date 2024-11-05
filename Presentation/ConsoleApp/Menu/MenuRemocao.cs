using System;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuRemocao : BaseMenu
    {
        private readonly IClienteRepository<Cliente> _clienteRepository;
        private readonly IImovelRepository _imovelRepository;

        public MenuRemocao(IClienteRepository<Cliente> clienteRepository, IImovelRepository imovelRepository)
        {
            _clienteRepository = clienteRepository;
            _imovelRepository = imovelRepository;
        }

        private void ExibirOpcoesRemocao()
        {
            Console.WriteLine("╔══════════════════════╦════════════════════╦════════════════╗");
            Console.WriteLine("   Buscar Clientes[1]    Buscar Imóveis[2]       \u001b[31mVoltar[0]\u001b[0m");
            Console.WriteLine("╚══════════════════════╩════════════════════╩════════════════╝");
        }

        public void ProcessarOpcoesDeRemocao(int opcao, ref bool sair)
        {
            switch (opcao)
            {
                case 1:
                    RemoverCliente();
                    break;
                case 2:
                    RemoverImovel();
                    break;
                case 0:
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                    break;
            }
        }

        public void RemoverCliente()
        {
            BoxDePesquisa("Digite o NOME:");
            var nome = Console.ReadLine();
            var cliente = _clienteRepository.BuscarPorNomeCliente(nome);

            if (cliente == null)
            {
                Console.SetCursorPosition(2, 14);
                Console.WriteLine("Cliente não encontrado.");
            }
            else
            {
                if (cliente is PessoaFisica pf)
                {
                    Console.SetCursorPosition(2, 9);
                    Console.WriteLine($"[{pf.Nome}] encontrado. \u001b[31mExcluir? (S/N)\u001b[0m");
                    Console.SetCursorPosition(2 + pf.Nome.Length, 11);
                    if (Console.ReadLine()?.Trim().ToUpper() == "S" ? true : false)
                    {
                        _clienteRepository.RemoverCliente(pf.Id);
                        Console.SetCursorPosition(2, 11);
                        Console.WriteLine($"\u001b[31m{pf.Nome} Cliente removido com sucesso!\u001b[0m");
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 11);
                        Console.WriteLine("                                            ");
                        Console.SetCursorPosition(6, 11);
                        Console.WriteLine("\u001b[32mOperação Cancelado.\u001b[0m");
                    }
                }
                else if (cliente is PessoaJuridica pj)
                {
                    Console.SetCursorPosition(2, 9);
                    Console.WriteLine($"[{pj.RazaoSocial}] encontrado. \u001b[31mExcluir? (S/N)\u001b[0m");
                    Console.SetCursorPosition(2 + pj.RazaoSocial.Length, 11);
                    if (Console.ReadLine()?.Trim().ToUpper() == "S" ? true : false)
                    {
                        _clienteRepository.RemoverCliente(pj.Id);
                        Console.SetCursorPosition(2, 11);
                        Console.WriteLine($"\u001b[31m{pj.RazaoSocial} Cliente removido com sucesso!\u001b[0m");
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 11);
                        Console.WriteLine("                                            ");
                        Console.SetCursorPosition(6, 11);
                        Console.WriteLine("\u001b[32mOperação Cancelado.\u001b[0m");
                    }
                }
                else
                {
                    Console.WriteLine($"Cliente não encontrado.");
                }
                Console.SetCursorPosition(2, 14);
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        public void RemoverImovel()
        {
            BoxDePesquisa("Digite o ENDEREÇO: ");
            //Console.Write("Digite a Inscrição IPTU do imóvel a ser removido: ");
            var inscricaoIptu = Console.ReadLine();
            var imovel = _imovelRepository.BuscarPorInscricaoIPTU(inscricaoIptu);

            if (imovel == null)
            {
                Console.WriteLine("Imóvel não encontrado.");
            }
            else
            {
                _imovelRepository.RemoverImovel(imovel.Id);
                Console.WriteLine("Imóvel removido com sucesso.");
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
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
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine();
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.SetCursorPosition(x, 11);
        }
    }
}
