﻿using ImobSys.Application.Ajuda;
using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Enums;
using ImobSys.Domain.Interfaces;
using ImobSys.Presentation.ConsoleApp.Handler;
using ImobSys.Presentation.ConsoleApp.Handlers;

namespace ImobSys.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository<Cliente> _clienteRepository;
        private readonly IImovelRepository _imovelRepository;
        private readonly OutputHandler _outputHandler;
        private readonly InputHandler _inputHandler;

        public ClienteService(IClienteRepository<Cliente> clienteRepository, IImovelRepository imovelRepository, OutputHandler outputHandler, InputHandler inputHandler)
        {
            _clienteRepository = clienteRepository;
            _imovelRepository = imovelRepository;
            _outputHandler = outputHandler;
            _inputHandler = inputHandler;
        }

        public (object cliente, List<Imovel> imoveis) ObterClienteESeusImoveis(string nomeCliente)
        {
            var imoveis = new List<Imovel>();

            var clienteId = _clienteRepository.ObterClientePorNome(nomeCliente);

            if (clienteId == null)
            {
                throw new Exception($"Cliente com nome '{nomeCliente}' não encontrado.");
            }
            
            imoveis = _imovelRepository.ObterImoveisPorCliente(clienteId);

            return (clienteId, imoveis);
        }

        public void CadastrarNovoCliente()
        {
            //Console.Clear();
            Console.SetCursorPosition(2, 9);
            Console.WriteLine("==== Cadastro de Novo Cliente ====");

            string tipoCliente = _inputHandler.SolicitarEntrada("Cliente (1)Pessoa Física / (2)Pessoa Jurídica? ", true);

            Cliente novoCliente;

            if (tipoCliente == "1")
            {
                novoCliente = CadastrarPessoaFisica();
            }
            else if (tipoCliente == "2")
            {
                novoCliente = CadastrarPessoaJuridica();
            }
            else
            {
                Console.WriteLine("Tipode de cliente inválido. Cadastro cancelado.");
                return;
            }

            _clienteRepository.SalvarCliente(novoCliente);
            Console.WriteLine("Cliente cadastrado com sucesso!");
            Console.WriteLine("\nPressione qualquer tecla para retornar ao menu...");
            Console.ReadKey();
        }

        public List<Cliente> ListarTodosClientes()
        {
            return _clienteRepository.ListarTodosClientes();
        }

        private PessoaFisica CadastrarPessoaFisica()
        {
            string nome = _inputHandler.SolicitarEntrada("Nome: ", true);
            string cpf = _inputHandler.SolicitarEntrada("CPF: ", true);
            string endereco = _inputHandler.SolicitarEntrada("Endereço (opcional): ");


            string telefone = _inputHandler.SolicitarEntrada("Telefone (opcional): ");

            Console.Write("Tipo de Relação (1) Locador / (2) Locatário / (3) Fiador: ");
            List<TiposRelacao> tipoRelacoes = ObterTiposRelacoes();

            return new PessoaFisica(nome, cpf, new Endereco { Logradouro = endereco }, tipoRelacoes)
            {
                Telefone = telefone
            };
        }

        private PessoaJuridica CadastrarPessoaJuridica()
        {
            string razaoSocial = _inputHandler.SolicitarEntrada("Razão Social: ", true);
            string cnpj = _inputHandler.SolicitarEntrada("CNPJ: ", true);
            string nomeRepresentante = _inputHandler.SolicitarEntrada("Nome do Representante (opcional): ");
            string inscricaoEstadual = _inputHandler.SolicitarEntrada("Inscrição Estadual (opcional): ");
            string endereco = _inputHandler.SolicitarEntrada("Endereço (opcional): ");
            string telefone = _inputHandler.SolicitarEntrada("Telefone (opcional): ");

            Console.Write("Tipo de Relação (1) Locador / (2) Locatário / (3) Fiador: ");
            List<TiposRelacao> tiposRelacoes = ObterTiposRelacoes();
            return new PessoaJuridica(razaoSocial, cnpj, new Endereco { Logradouro = endereco }, tiposRelacoes, nomeRepresentante, inscricaoEstadual)
            {
                Telefone = telefone
            };
        }

        private List<TiposRelacao> ObterTiposRelacoes()
        {
            var tipoRelacoes = new List<TiposRelacao>();

            bool adicionarMais;
            do
            {
                Console.WriteLine("Escolha o Tipo de Relação:");
                Console.WriteLine("1. Locador");
                Console.WriteLine("2. Locatário");
                Console.WriteLine("3. Fiador");
                Console.Write("Opção: ");

                if (Enum.TryParse(Console.ReadLine(), out TiposRelacao tipoRelacao))
                {
                    tipoRelacoes.Add(tipoRelacao);
                    Console.Write("Deseja adicionar outra relação? (S/N): ");
                    adicionarMais = Console.ReadLine()?.Trim().ToUpper() == "S";
                }
                else
                {
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    adicionarMais = true;
                }
            } while (adicionarMais);

            return tipoRelacoes;
        }

        public void RemoverCliente(string nomeCliente)
        {
            try
            {
                var clienteId = _clienteRepository.ObterClientePorNome(nomeCliente);

                var sucesso = _clienteRepository.RemoverCliente(clienteId);

                if (sucesso)
                {
                    _outputHandler.ExibirSucesso($"Cliente '{nomeCliente}' removido com sucesso!");
                }
            }
            catch (Exception ex) {
                Console.SetCursorPosition(2, 7);
                _outputHandler.ExibirErro($"Erro: {ex.Message} Operação Cancelada.");
                
                Console.SetCursorPosition(2, 9);
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu...");
                Console.ReadKey();
            }
        }
    }
}
