using ImobSys.Domain;
using ImobSys.Domain.Entities;
using ImobSys.Domain.Enums;

namespace ImobSys.Presentation.Handler
{
    public class UserInteractionHandler
    {
        // ====================== AVISOS AO USUÁRIO ====================== //
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


        // ====================== TIPOS DE DADOS REQUERIDOS ====================== //
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

        public string SolicitarCampo(string mensagem, bool obrigatorio = true, ConsoleColor cor = ConsoleColor.White)
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
                Console.Write(mensagem);
                if (int.TryParse(Console.ReadLine(), out int numero) && numero >= 0)
                    return numero;

                Console.Write("Entrada inválida! Digite um número positivo: ");
            }
        }


        // ====================== COLETAR DADOS DO IMÓVEL ====================== //
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

            imovel.Quartos = LerIntPositivo("Número de Quartos: ");
            imovel.Salas = LerIntPositivo("Número de Salas: ");
            imovel.Banheiros = LerIntPositivo("Número de Banheiros: ");
            imovel.Garagens = LerIntPositivo("Número de Garagens: ");
            imovel.Cozinha = SolicitarCaracteristicaBoolean("Possui Cozinha? [1]Sim / [2]Não: ");
            imovel.Copa = SolicitarCaracteristicaBoolean("Possui Copa? [1]Sim / [2]Não: ");
            imovel.Quintal = SolicitarCaracteristicaBoolean("Possui Quintal? [1]Sim / [2] Não: ");
        }

        public Endereco ObterEndereco()
        {
            ExibirMensagem("\n==== Informações de Endereço ====\n", ConsoleColor.Cyan);
            var confirmaEndereco = false;
            Endereco endereco;

            do
            {
                var tipoLogradouro = SolicitarTipoLogradouro().ToString();
                var logradouro = SolicitarCampo($" {tipoLogradouro}:", true, ConsoleColor.Cyan);
                var numero = SolicitarCampo($" {tipoLogradouro} {logradouro} Número:", true, ConsoleColor.Cyan);
                var complemento = SolicitarCampo($" {tipoLogradouro} {logradouro} {numero} complemento:", false, ConsoleColor.Cyan);

                endereco = new Endereco()
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

                var enderecoFormatado = FormatarEndereco(endereco);
                confirmaEndereco = SolicitarConfirmacao($"\nEndereço está correto? \u001b[31m{enderecoFormatado}\u001b[0m");

            } while (!confirmaEndereco);

            return endereco;


            string FormatarEndereco(Endereco endereco)
            {
                return $"{endereco.TipoLogradouro} {endereco.Logradouro}, {endereco.Numero} {endereco.Complemento}, " +
                       $"{endereco.Bairro}, {endereco.Cidade} - {endereco.UF}, CEP: {endereco.CEP}";
            }
        }

        public string ObterTipoImovel()
        {
            Console.WriteLine("Selecione o Tipo de Imóvel:", ConsoleColor.Cyan);

            var listaDeSubTiposImoveis = Enum.GetValues(typeof(SubtipoImovel));
            int colunas = 4;
            int count = 0;

            foreach (var tipo in listaDeSubTiposImoveis)
            {
                var colorNumber = $"\u001b[31m{(int)tipo}\u001b[0m";

                Console.Write($"[{colorNumber}] {tipo,-8}"); // Alinha o texto com largura fixa

                count++;

                if (count % colunas == 0)
                {
                    Console.WriteLine();
                }
            }

            if (count % colunas != 0)
            {
                Console.WriteLine();
            }

            return SolicitarEntrada("Tipo de Imóvel:", true);
        }
        
        public TipoLogradouro SolicitarTipoLogradouro()
        {
            int colunas = 5;
            int count = 0;

            foreach (var tipo in Enum.GetValues(typeof(TipoLogradouro)))
            {
                var colorNumber = $"\u001b[31m{(int)tipo}\u001b[0m";

                Console.Write($" [{colorNumber}] {tipo,-8}");
                count++;

                if (count == colunas)
                {
                    Console.WriteLine();
                }
            }
            Console.Write("\nEscolha o tipo de Logradouro: ", ConsoleColor.Cyan);

            int escolha;
            while (!int.TryParse(Console.ReadLine(), out escolha) || !Enum.IsDefined(typeof(TipoLogradouro), escolha))
            {
                Console.WriteLine("Opção inválida! Digite o número correspondente a uma opção da lista.");
            }

            return (TipoLogradouro)escolha;
        }
    }
}
