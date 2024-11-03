using System;
using ImobSys.Domain;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Interfaces;

namespace ImobSys.Presentation.ConsoleApp.Menu
{
    public class ConsoleDataLister
    {
        private readonly IClienteRepository<Cliente> _clienteRepository;
        private readonly IImovelRepository _imovelRepository;

        public ConsoleDataLister(IClienteRepository<Cliente> clienteRepository, IImovelRepository imovelRepository)
        {
            _clienteRepository = clienteRepository;
            _imovelRepository = imovelRepository;
        }

        public void ListarTodosClientes()
        {
            var clientes = _clienteRepository.ListarTodosCliente();
            if (clientes.Count > 0)
            {
                Console.WriteLine("\n\n\u001b[33mClientes cadastrados:\u001b[0m");

                // Imprime o cabeçalho da tabela
                Console.WriteLine("╔═════════════════════════════════════╦══════════════════════╗");
                Console.WriteLine("║                NOME                 ║      CPF/CNPJ        ║");
                Console.WriteLine("╠═════════════════════════════════════╬══════════════════════╣");

                string nomeFormatado;

                foreach (var cliente in clientes)
                {
                    if (cliente is PessoaFisica pessoaFisica)
                    {
                        nomeFormatado = pessoaFisica.Nome.Length > 35
                        ? pessoaFisica.Nome.Substring(0, 32) + "..."
                        : pessoaFisica.Nome.PadRight(35);

                        string cpfFormatada = pessoaFisica.CPF;
                        if (cpfFormatada.Length == 11)
                        {
                            cpfFormatada = cpfFormatada.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                        }
                        Console.WriteLine($"║ {nomeFormatado,-35} ║ {cpfFormatada,-20} ║");
                    }
                    else if (cliente is PessoaJuridica pessoaJuridica)
                    {
                        nomeFormatado = pessoaJuridica.RazaoSocial.Length > 35
                        ? pessoaJuridica.RazaoSocial.Substring(0, 32) + "..."
                        : pessoaJuridica.RazaoSocial.PadRight(35);


                        Console.WriteLine($"║ {nomeFormatado,-4} ║ {pessoaJuridica.CNPJ,-20} ║");
                    }
                }
                Console.WriteLine("╚═════════════════════════════════════╩══════════════════════╝");
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            Console.WriteLine("\n\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            ;
        }

        public void ListarTodosImoveis()
        {
            var imoveis = _imovelRepository.ListarTodosImovel();
            if (imoveis.Count > 0)
            {
                Console.WriteLine("\n\n\u001b[33mImóveis cadastrados:\u001b[0m");
                foreach (var imovel in imoveis)
                {
                    Console.WriteLine($"Imóvel: {imovel.Endereco.TipoLogradouro} {imovel.Endereco.Logradouro}, {imovel.Endereco.Numero} {imovel.Endereco.Complemento}, Tipo: {imovel.TipoImovel}, Área: {imovel.AreaUtil} m²");
                }
            }
            else
            {
                Console.WriteLine("Nenhum imóvel cadastrado.");
            }
            Console.WriteLine("\n\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void ListarTodosIPTUs()
        {
            var imoveis = _imovelRepository.ListarTodosImovel();
            if (imoveis.Count > 0)
            {
                Console.WriteLine("\n\n\u001b[33mIPTUs cadastrados:\u001b[0m");
                foreach (var imovel in imoveis)
                {
                    string inscricaoIPTUFormatada = imovel.InscricaoIPTU;

                    if (inscricaoIPTUFormatada.Length == 9)
                    {
                        inscricaoIPTUFormatada = inscricaoIPTUFormatada.Insert(3, ".").Insert(7, "/");
                    }
                    Console.WriteLine($"IPTU: {inscricaoIPTUFormatada} Enderço: {imovel.Endereco.TipoLogradouro} {imovel.Endereco.Logradouro}, {imovel.Endereco.Numero} {imovel.Endereco.Complemento}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum imóvel cadastrado.");
            }
            Console.WriteLine("\n\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
