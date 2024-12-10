using ImobSys.Domain;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Enums;
using System;

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
            //int linhaParaExibir = MenuStateManager.Instance.ObterProximaLinha();

            Console.SetCursorPosition(2, Console.CursorTop + 1);

            Console.ForegroundColor = cor;
            Console.Write(mensagem);
            Console.ResetColor();
        }

        public void ExibirErro(string mensagem)
        {
            int linhaParaExibir = MenuStateManager.Instance.ObterProximaLinha() + 2;
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
            int linhaParaExibir = MenuStateManager.Instance.ObterProximaLinha() + 2;

            Console.SetCursorPosition(2, linhaParaExibir);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{mensagem}");
            Console.ResetColor();
        }

        public string ConfigurarTipoImovel()
        {
            string tipoImovel = "";
            do
            {
                MenuStateManager.Instance.ObterProximaLinha();

                ExibirMensagem("Tipo do Imóvel \n    (1)Residencial\n    (2)Comercial\n    (3)Misto)\n          Opção: ");
                if (int.TryParse(Console.ReadLine(), out int escolha))
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
            while (true)
            {
                var inscricaoIPTU = SolicitarCampo("Inscrição de IPTU: ", false);

                if (!string.IsNullOrWhiteSpace(inscricaoIPTU) && inscricaoIPTU.Length == 9)
                {
                    return inscricaoIPTU ?? string.Empty;
                }
                ExibirErro("Número de inscrição Inválida!");
            }
        }
        public float ObterAreaUtil()
        {
            float areaUtil;

            Console.Write("Área Útil (m²): ");
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
            Console.WriteLine("\n==== Informações de Endereço ====");
            var endereco = new Endereco()
            {
                TipoLogradouro = SolicitarTipoLogradouro().ToString(),
                Logradouro = SolicitarCampo("Logradouro:"),
                Numero = SolicitarCampo("Número:"),
                Complemento = SolicitarCampo("complemento:", false),
                Bairro = SolicitarCampo("Bairro:"),
                Cidade = "Juiz de Fora", // Pode ser parametrizado ou solicitado
                UF = "MG", // Pode ser parametrizado ou solicitado
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
            Console.WriteLine("Selecione o Tipo de Imvel:");

            var listaDeSubTiposImoveis = Enum.GetValues(typeof(SubtipoImovel));
            int count = 0;

            foreach (var tipo in listaDeSubTiposImoveis)
            {
                Console.Write($" [{(int)tipo}] {tipo}");
                count++;

                if (count == 4)
                {
                    Console.WriteLine();
                    count = 0;
                }
            }

            return SolicitarEntrada("\nTipo de Imóvel:", true);
        }

        public TipoLogradouro SolicitarTipoLogradouro()
        {
            Console.WriteLine("Selecione o Tipo de Logradouro:");
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
            Console.Write("O imóvel está disponível para locação? (S/N): ");
            imovel.ParaLocacao = Console.ReadLine()?.Trim().ToUpper() == "S";
            if (imovel.ParaLocacao)
            {
                Console.Write("Status ATUAL (1) Disponível / (2) Alugado: ");
                imovel.StatusLocacao = int.TryParse(Console.ReadLine(), out int status) && status == 2 ? "Alugado" : "Disponível";

                Console.Write("Valor do Aluguel: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal aluguel))
                {
                    imovel.ValorAluguel = aluguel;
                }
            }

            Console.Write("O imóvel está disponível para venda? (S/N): ");
            imovel.ParaVenda = Console.ReadLine()?.Trim().ToUpper() == "S";
            if (imovel.ParaVenda)
            {
                Console.Write("Valor de Venda: ");
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
                Console.Write("(1) Sim / (2) Não: ");
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

        private int LerIntPositivo()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int numero) && numero >= 0)
                    return numero;

                Console.Write("Entrada inválida! Digite um número positivo: ");
            }
        }
    }
}
