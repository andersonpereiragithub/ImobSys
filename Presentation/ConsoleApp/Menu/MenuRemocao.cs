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
            //Console.Write("Digite o nome do cliente a ser removido: ");
            var nome = Console.ReadLine();
            var cliente = _clienteRepository.BuscarPorNomeCliente(nome);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
            }
            else 
            {
                if (cliente is PessoaFisica pf)
                {
                    if (cliente != null)
                    {
                        Console.WriteLine($"Cliente: {pf.Nome} encontrado. Tem certeza que deseja excluir? (S/N)");
                        Console.WriteLine(Console.ReadLine()?.Trim().ToUpper() == "S" ? "Cliente excluído." : "Operação cancelada.");
                    }
                    var removido = _clienteRepository.RemoverCliente(pf.Id);
                    Console.WriteLine($"{removido} Cliente removido com sucesso!");
                }
                else if (cliente is PessoaJuridica pj)
                {
                    if (cliente != null)
                    {
                        Console.WriteLine($"Cliente: {pj.RazaoSocial} encontrado. Tem certeza que deseja excluir? (S/N)");
                        Console.WriteLine(Console.ReadLine()?.Trim().ToUpper() == "S" ? "Cliente excluído." : "Operação cancelada.");
                    }
                    var removido = _clienteRepository.RemoverCliente(pj.Id);
                    Console.WriteLine($"{removido} Cliente removido com sucesso!");
                }
                else
                {
                    Console.WriteLine($"Cliente não encontrado.");
                }
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
