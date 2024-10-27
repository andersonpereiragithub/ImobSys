using ImobSys.Domain.Interfaces;
using ImobSys.Application.Services;
using System;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class MenuCadastro
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IImovelRepository _imovelRepository;
        private readonly ClienteService _clienteService;
        private readonly ImovelService _imovelService;

        public MenuCadastro(IClienteRepository clienteRepository, IImovelRepository imovelRepository, ClienteService clienteService, ImovelService imovelService)
        {
            _clienteRepository = clienteRepository;
            _imovelRepository = imovelRepository;
            _clienteService = clienteService;
            _imovelService = imovelService;
        }

        public void ExibirMenuCadastro()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=========== MENU DE CADASTRO ===========");
                Console.WriteLine("1. Cadastrar Novo Cliente");
                Console.WriteLine("2. Cadastrar Novo Imóvel");
                Console.WriteLine("0. Voltar");
                Console.WriteLine("========================================");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        _clienteService.CadastrarNovoCliente();
                        break;
                    case "2":
                        _imovelService.CadastrarNovoImovel();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //private void CadastrarNovoCliente()
        //{
        //    // Implementação ou chamada para cadastrar cliente
        //    Console.WriteLine("Função de cadastro de cliente...");
        //    Console.ReadKey();
        //}

        //private void CadastrarNovoImovel()
        //{
        //    // Implementação ou chamada para cadastrar imóvel
        //    Console.WriteLine("Função de cadastro de imóvel...");
        //    Console.ReadKey();
        //}
    }
}
