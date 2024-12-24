using ImobSys.Domain;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Enums;
using ImobSys.Domain.Interfaces;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;

namespace ImobSys.Presentation.Handler
{
    public class UserInteractionHandler
    {
        
        public int SolicitarOpcaoNumerica(string mensagem, int min, int max)
        {
            while (true)
            {
                ExibirMensagem($"{mensagem} [{min}-{max}]: ", ConsoleColor.Cyan);
                if (int.TryParse(Console.ReadLine(), out int opcao) && opcao >= min && opcao <= max)
                {
                    return opcao;
                }
                ExibirErro($"Entrada inválida! Digite um número entre {min} e {max}.");
            }
        }

        public string SolicitarEntrada(string mensagem, bool permitirVazio = false)
        {
            while (true)
            {
                ExibirMensagem($"{mensagem} ", ConsoleColor.Cyan);
                var entrada = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(entrada) || !permitirVazio)
                {
                    return entrada;
                }
                ExibirErro("Entrada não pode ser vazia. Tente novamente.");
            }
        }

        public bool SolicitarConfirmacao(string mensagem)
        {
            while (true)
            {
                ExibirMensagem($"{mensagem} (S/N): ", ConsoleColor.Cyan);
                var entrada = Console.ReadLine()?.Trim().ToUpper();

                if (entrada == "S") return true;
                if (entrada == "N") return false;

                ExibirErro("Entrada inválida! Digite 'S' para Sim ou 'N' para Não.");
            }
        }

        public void ExibirMensagem(string mensagem, ConsoleColor cor = ConsoleColor.White)
        {
            Console.SetCursorPosition(2, Console.CursorTop + 1);

            Console.ForegroundColor = cor;
            Console.Write(mensagem);
            Console.ResetColor();
        }

        public void ExibirErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro: {mensagem}");
            Console.ResetColor();
        }

        public void ExibirSucesso(string mensagem)
        {
            ExibirMensagem($"Sucesso: {mensagem}", ConsoleColor.Green);
        }

        public void ExibirMensagemRetornoMenu(string mensagem)
        {
            Console.SetCursorPosition(2, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{mensagem}");
            Console.ResetColor();
            Console.ReadKey();
        }

        public string ConfigurarTipoImovel()
        {
            string tipoImovel = "";
            do
            {
                int escolha = SolicitarOpcaoNumerica("Tipo do Imóvel \n    (1)Residencial\n    (2)Comercial\n    (3)Misto)\n          Opção: ", 1, 3);
                if (escolha > 0 && escolha <= 3)
                {
                    tipoImovel = escolha switch
                    {
                        1 => "Residencial",
                        2 => "Comercial",
                        3 => "Misto",
                        _ => tipoImovel
                    };
                    if (!string.IsNullOrEmpty(tipoImovel)) break;
                }
                Console.WriteLine("Opção Inválida! Escolha 1, 2 ou 3.");
            } while (true);
            return tipoImovel;
        }

        public string ObterInscricaoIPTU()
        {

            string novaInscricaoIPTU;
            ExibirMensagem("Inscrição de IPTU: ", ConsoleColor.Cyan);
            
            while (string.IsNullOrWhiteSpace(novaInscricaoIPTU = Console.ReadLine()))
            {
                Console.Write("Número de inscrição Inválida! Digite novamente: ");
            }
            return novaInscricaoIPTU;
        }

        public float ObterAreaUtil()
        {
            float areaUtil;

            ExibirMensagem("Área Útil (m²): ", ConsoleColor.Cyan);
            while (!float.TryParse(Console.ReadLine(), out areaUtil))
            {
                Console.Write("Entrada inválida para área útil. Digite novamente: ");
            }
            return areaUtil;
        }

        public void ConfigurarCaracteristicasInternas(Imovel imovel)
        {
            Console.WriteLine("\n==== Configuração de Características Internas ====");

            imovel.Quartos = SolicitarCaracteristicaNumerica("Número de Quartos: ");
            imovel.Salas = SolicitarCaracteristicaNumerica("Número de Salas: ");
            imovel.Banheiros = SolicitarCaracteristicaNumerica("Número de Banheiros: ");
            imovel.Garagens = SolicitarCaracteristicaNumerica("Número de Garagens: ");
            imovel.Cozinha = SolicitarCaracteristicaBoolean("Possui Cozinha? [1]Sim / [2]Não: ");
            imovel.Copa = SolicitarCaracteristicaBoolean("Possui Copa? [1]Sim / [2]Não: ");
            imovel.Quintal = SolicitarCaracteristicaBoolean("Possui Quintal? [1]Sim / [2] Não: ");
        }

        public Endereco ObterEndereco()
        {
            ExibirMensagem("==== Informações de Endereço ====", ConsoleColor.Cyan);

            var tipoLogradouro = SolicitarTipoLogradouro().ToString();
            var logradouro = SolicitarCampo($"Logradouro {tipoLogradouro}:");
            var numero = SolicitarCampo($"{tipoLogradouro} {logradouro} Número:");
            var complemento = SolicitarCampo($"{tipoLogradouro} {logradouro} {numero} complemento:", false);
            
            var endereco = new Endereco()
            {
                TipoLogradouro = tipoLogradouro,
                Logradouro = logradouro,
                Numero = numero,
                Complemento = complemento,
                Bairro = SolicitarCampo("Bairro:"),
                Cidade = "Juiz de Fora", 
                UF = "MG", 
                CEP = SolicitarCampo("CEP:")
            };

            Console.WriteLine($"\nEndereço completo: {FormatarEndereco(endereco)}");

            return endereco;

            string FormatarEndereco(Endereco endereco)
            {
                return $"{endereco.TipoLogradouro} {endereco.Logradouro}, {endereco.Numero} {endereco.Complemento}, " +
                       $"{endereco.Bairro}, {endereco.Cidade} - {endereco.UF}, CEP: {endereco.CEP}";
            }
        }

        public string ObterTipoImovel()
        {
            ExibirMensagem("Selecione o Tipo de Imvel:", ConsoleColor.Cyan);

            var listaDeSubTiposImoveis = Enum.GetValues(typeof(SubtipoImovel));
            int count = 0;

            foreach (var tipo in listaDeSubTiposImoveis)
            {
                var colorNumber = $"\u001b[31m{(int)tipo}\u001b[0m";

                ExibirMensagem($" [{colorNumber}]");
                ExibirMensagem($"{tipo}", ConsoleColor.Cyan);
            }

            return SolicitarEntrada("Tipo de Imóvel:", true);
        }

        public TipoLogradouro SolicitarTipoLogradouro()
        {
            ExibirMensagem("Selecione o Tipo de Logradouro:", ConsoleColor.Cyan);
            int count = 0;

            foreach (var tipo in Enum.GetValues(typeof(TipoLogradouro)))
            {
                Console.Write($" [{(int)tipo}] {tipo,-8}");
                count++;

                if (count == 5)
                {
                    Console.WriteLine();
                    count = 0;
                }
            }
            Console.Write("Escolha o tipo de Logradouro: ");

            int escolha;
            while (!int.TryParse(Console.ReadLine(), out escolha) || !Enum.IsDefined(typeof(TipoLogradouro), escolha))
            {
                Console.WriteLine("Opção inválida! Digite o número correspondente a uma opção da lista.");
            }

            return (TipoLogradouro)escolha;
        }

        public void ConfigurarLocacaoEVenda(Imovel imovel)
        {
            ExibirMensagem("O imóvel está disponível para locação? (S/N): ", ConsoleColor.Cyan);
            imovel.ParaLocacao = Console.ReadLine()?.Trim().ToUpper() == "S";

            if (imovel.ParaLocacao)
            {
                ExibirMensagem("Status ATUAL (1) Disponível / (2) Alugado: ", ConsoleColor.Cyan);
                imovel.StatusLocacao = int.TryParse(Console.ReadLine(), out int status) && status == 2 ? "Alugado" : "Disponível";

                ExibirMensagem("Valor do Aluguel: ", ConsoleColor.Cyan);
                if (decimal.TryParse(Console.ReadLine(), out decimal aluguel))
                {
                    imovel.ValorAluguel = aluguel;
                }
            }

            ExibirMensagem("O imóvel está disponível para venda? (S/N): ", ConsoleColor.Cyan);
            imovel.ParaVenda = Console.ReadLine()?.Trim().ToUpper() == "S";

            if (imovel.ParaVenda)
            {
                ExibirMensagem("Valor de Venda: ", ConsoleColor.Cyan);
                if (decimal.TryParse(Console.ReadLine(), out decimal venda))
                {
                    imovel.ValorVenda = venda;
                }
            }
        }

        public string SolicitarCampo(string mensagem, bool obrigatorio = true)
        {
            while (true)
            {
                Console.Write(mensagem + " ");
                var entrada = Console.ReadLine();

                if (!obrigatorio || !string.IsNullOrWhiteSpace(entrada))
                {
                    return entrada ?? string.Empty;
                }

                Console.WriteLine("Este campo é obrigatório. Tente novamente.");
            }
        }

        public bool LerOpcaoSimNao(string mensagem = "")
        {
            while (true)
            {
                Console.Write($"{mensagem} (1) Sim / (2) Não: ");
                if (int.TryParse(Console.ReadLine(), out int escolha) && (escolha == 1 || escolha == 2))
                {
                    return escolha == 1;
                }
                Console.WriteLine("Opção inválida! Digite 1 para Sim ou 2 para Não.");
            }
        }

        private int SolicitarCaracteristicaNumerica(string mensagem)
        {
            ExibirMensagem(mensagem);
            return LerIntPositivo();
        }

        private bool SolicitarCaracteristicaBoolean(string mensagem)
        {
            Console.WriteLine(mensagem);
            return LerOpcaoSimNao();
        }

        public int LerIntPositivo(string mensagem = "")
        {
            while (true)
            {
                Console.WriteLine(mensagem);
                if (int.TryParse(Console.ReadLine(), out int numero) && numero >= 0)
                    return numero;

                Console.Write("Entrada inválida! Digite um número positivo: ");
            }
        }
    }
}
