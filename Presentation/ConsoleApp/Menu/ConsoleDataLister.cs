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
                foreach (var cliente in clientes)
                {
                    if (cliente is PessoaFisica pessoaFisica)
                    {
                        string cpfFormatada = pessoaFisica.CPF;

                        if (cpfFormatada.Length == 11)
                        {
                            cpfFormatada = cpfFormatada.Insert(3, ".").Insert(7, ".").Insert(11,"-");
                        }
                        Console.WriteLine($"Nome: {cliente.Nome}, CPF: {cpfFormatada} ");
                    }
                    else if (cliente is PessoaJuridica pessoaJuridica)
                    {
                        Console.WriteLine($"Razão Social: {pessoaJuridica.RazaoSocial}, CNPJ: {pessoaJuridica.CNPJ}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            Console.WriteLine("\n\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
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
