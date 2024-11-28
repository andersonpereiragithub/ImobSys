using ImobSys.Application.Services.Interfaces;
using ImobSys.Domain.Entities.Clientes;
using ImobSys.Presentation.Handler;

namespace ImobSys.Presentation.Menu
{
    public class ConsoleDataLister : BaseMenu
    {
        private readonly IClienteService _clienteService;
        private readonly IImovelService _imovelService;
        private readonly UserInteractionHandler _userInteractionHandler;

        public ConsoleDataLister(IClienteService clienteService, IImovelService movelService, UserInteractionHandler userInteractionHandler)
        {
            _clienteService = clienteService;
            _imovelService = movelService;
            _userInteractionHandler = userInteractionHandler;
        }

        public void ExibirTabela(List<string> cabecalhos, List<List<string>> dados, List<bool> alinhamenstosDireita)
        {
            int larguraTotal = 102;
            int espacoEntreColunas = 3;
            List<int> largurasColunas = CalcularLargurasColunas(cabecalhos, dados, larguraTotal, espacoEntreColunas);

            ExibirLinhaBordaSuperior(largurasColunas);
            ExibirCabecalhos(cabecalhos, largurasColunas);
            ExibirLinhaBordaSeparadora(largurasColunas);
            ExibirDados(dados, largurasColunas, alinhamenstosDireita);
            ExibirLinhaBordaInferior(largurasColunas);
        }

        private List<int> CalcularLargurasColunas(List<string> cabecalhos, List<List<string>> dados, int larguraTotal, int espacoEntreColunas)
        {
            List<int> largurasColunas = new List<int>();

            for (int i = 0; i < cabecalhos.Count; i++)
            {
                int larguraCabecalho = cabecalhos[i].Length;
                int larguraMaximaDados = dados.Max(l => i < l.Count ? l[i].Length : 0);
                int larguraColuna = Math.Max(larguraCabecalho, larguraMaximaDados) + espacoEntreColunas;
                largurasColunas.Add(larguraColuna);
            }

            AjustarLargurasColunas(largurasColunas, larguraTotal);
            return largurasColunas;
        }

        private void AjustarLargurasColunas(List<int> largurasColunas, int larguraTotal)
        {
            int larguraSomada = largurasColunas.Sum() + largurasColunas.Count + 1;

            if (larguraSomada < larguraTotal)
            {
                int espacoExtra = larguraTotal - larguraSomada;
                int espacoExtraPorColuna = espacoExtra / largurasColunas.Count;
                for (int i = 0; i < largurasColunas.Count; i++)
                {
                    largurasColunas[i] += espacoExtraPorColuna;
                }
            }
            else if (larguraSomada > larguraTotal)
            {
                double fatorAjuste = (double)larguraTotal / larguraSomada;
                for (int i = 0; i < largurasColunas.Count; i++)
                {
                    largurasColunas[i] = (int)(largurasColunas[i] * fatorAjuste);
                }
            }
        }

        private void ExibirLinhaBordaSuperior(List<int> largurasColunas)
        {
            Console.WriteLine("╔" + string.Join("╦", largurasColunas.Select(l => new string('═', l))) + "╗");
        }

        private void ExibirCabecalhos(List<string> cabecalhos, List<int> largurasColunas)
        {
            for (int i = 0; i < cabecalhos.Count; i++)
            {
                int espacoParaEsquerda = (largurasColunas[i] - cabecalhos[i].Length) / 2;
                string textoCabecalho = cabecalhos[i].PadLeft(cabecalhos[i].Length + espacoParaEsquerda).PadRight(largurasColunas[i]);
                Console.Write($"║{textoCabecalho}");
            }
            Console.WriteLine("║");
        }

        private void ExibirLinhaBordaSeparadora(List<int> largurasColunas)
        {
            Console.WriteLine("╠" + string.Join("╬", largurasColunas.Select(l => new string('═', l))) + "╣");
        }

        private void ExibirDados(List<List<string>> dados, List<int> largurasColunas, List<bool> alinhamentosDireita)
        {
            foreach (var linha in dados)
            {
                for (int i = 0; i < largurasColunas.Count; i++)
                {
                    string textoColuna = i < linha.Count ? linha[i] : "";
                    int largura = largurasColunas[i] - 1;

                    if (alinhamentosDireita[i])
                    {
                        textoColuna = textoColuna.PadLeft(largura).PadRight(largurasColunas[i]);
                    }
                    else
                    {
                        textoColuna = textoColuna.PadRight(largura).PadLeft(largurasColunas[i]);
                    }
                    Console.Write($"║{textoColuna}");
                }
                Console.WriteLine("║");
            }
        }

        private void ExibirLinhaBordaInferior(List<int> largurasColunas)
        {
            Console.WriteLine("╚" + string.Join("╩", largurasColunas.Select(l => new string('═', l))) + "╝");
        }

        public void ListarTodosClientes()
        {
            var clientes = _clienteService.ListarTodosClientes();

            if (clientes.Count > 0)
            {
                Console.WriteLine("\n\n\u001b[33mClientes cadastrados:\u001b[0m");

                List<string> cabecalhos = new List<string> { "NOME", "CPF/CNPJ" };

                List<List<string>> dados = new List<List<string>>();

                List<bool> alinhamentosDireita = new List<bool> { false, true };

                foreach (var cliente in clientes)
                {
                    if (cliente is PessoaFisica pessoaFisica)
                    {
                        string nomeFormatado = pessoaFisica.Nome.Length > 35
                            ? pessoaFisica.Nome.Substring(0, 32) + "..."
                            : pessoaFisica.Nome;

                        string cpfFormatada = pessoaFisica.CPF.Length == 11
                            ? pessoaFisica.CPF.Insert(3, ".").Insert(7, ".").Insert(11, "-")
                            : pessoaFisica.CPF;

                        dados.Add(new List<string> { nomeFormatado, cpfFormatada });
                    }
                    else if (cliente is PessoaJuridica pessoaJuridica)
                    {
                        string nomeFormatado = pessoaJuridica.RazaoSocial.Length > 35
                            ? pessoaJuridica.RazaoSocial.Substring(0, 32) + "..."
                            : pessoaJuridica.RazaoSocial;

                        string cnpjFormatada = pessoaJuridica.CNPJ.Length == 14
                            ? pessoaJuridica.CNPJ.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-")
                            : pessoaJuridica.CNPJ;

                        dados.Add(new List<string> { nomeFormatado, cnpjFormatada });
                    }
                }
                dados = dados.OrderBy(d => d[0]).ToList();

                ExibirTabela(cabecalhos, dados, alinhamentosDireita);
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }

            Console.WriteLine("\n\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void ExibirListaDeTodosImoveis()
        {
            var imoveis = _imovelService.ListarTodosImoveis();

            if (imoveis.Count > 0)
            {
                List<string> cabecalhos = new List<string> { "Imóvel", "Tipo", "Área(m²)", "V Aluguel" };

                List<List<string>> dados = new List<List<string>>();

                List<bool> alinhamentosDireita = new List<bool> { false, false, true, true };

                Console.WriteLine("\n\n\u001b[33mImóveis cadastrados:\u001b[0m");
                foreach (var imovel in imoveis)
                {
                    string endereco = $"{imovel.Endereco.TipoLogradouro} " +
                                      $"{imovel.Endereco.Logradouro} " +
                                      $"{imovel.Endereco.Numero} " +
                                      $"{imovel.Endereco.Complemento}";
                    string tipo = $"{imovel.TipoImovel}";
                    string area = $"{imovel.AreaUtil}";
                    string valorAluguel = imovel.ValorAluguel != null ? $"{imovel.ValorAluguel}" : "-";

                    dados.Add(new List<string> { endereco, tipo, area, valorAluguel });
                }
                dados = dados.OrderBy(d => d[0]).ToList();

                ExibirTabela(cabecalhos, dados, alinhamentosDireita);
            }
            else
            {
                Console.WriteLine("Nenhum imóvel cadastrado.");
            }
            Console.WriteLine("\n\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void ExibirListaTodosIPTUs()
        {
            var imoveis = _imovelService.ListarTodosImoveis();
            if (imoveis.Count > 0)
            {
                List<string> cabecalhos = new List<string> { "IPTU", "ENDEREÇO" };

                List<List<string>> dados = new List<List<string>>();

                List<bool> alinhamentosDireita = new List<bool> { true, false };

                Console.WriteLine("\n\n\u001b[33mIPTUs cadastrados:\u001b[0m");
                foreach (var imovel in imoveis)
                {
                    string endereco = $"{imovel.Endereco.TipoLogradouro} " +
                                      $"{imovel.Endereco.Logradouro} " +
                                      $"{imovel.Endereco.Numero} " +
                                      $"{imovel.Endereco.Complemento}";

                    string inscricaoIPTUFormatada = imovel.InscricaoIPTU != null ? imovel.InscricaoIPTU : "000000000";

                    if (inscricaoIPTUFormatada.Length == 9)
                    {
                        inscricaoIPTUFormatada = inscricaoIPTUFormatada.Insert(3, ".").Insert(7, "/");
                    }
                    dados.Add(new List<string> { inscricaoIPTUFormatada, endereco });
                }
                ExibirTabela(cabecalhos, dados, alinhamentosDireita);
            }
            else
            {
                Console.WriteLine("Nenhum imóvel cadastrado.");
            }
            Console.WriteLine("\n\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void ExibirProprietarioEListarSeusImoveis()
        {
            Console.SetCursorPosition(2, 7);
            Console.Write("Informe nome do Proprietário: ");
            var nomeProprietario = Console.ReadLine();
            try
            {
                var (cliente, imoveis) = _clienteService.ObterClienteESeusImoveis(nomeProprietario);

                List<string> cabecalhos = new List<string> { "Inscrição IPTU", "Endereço" };

                List<List<string>> dados = new List<List<string>>();

                List<bool> alinhamentosDireita = new List<bool> { true, false };

                foreach (var imovel in imoveis)
                {
                    string endereco = $"{imovel.Endereco.TipoLogradouro} " +
                                      $"{imovel.Endereco.Logradouro} " +
                                      $"{imovel.Endereco.Numero} " +
                                      $"{imovel.Endereco.Complemento}";

                    string inscricaoIPTUFormatada = imovel.InscricaoIPTU != null ? imovel.InscricaoIPTU : "000000000";

                    if (inscricaoIPTUFormatada.Length == 9)
                    {
                        inscricaoIPTUFormatada = inscricaoIPTUFormatada.Insert(3, ".").Insert(7, "/");
                    }
                    dados.Add(new List<string> { inscricaoIPTUFormatada, endereco });
                }
                Console.SetCursorPosition(0, 9);
                ExibirTabela(cabecalhos, dados, alinhamentosDireita);
            }
            catch (Exception ex)
            {
                _userInteractionHandler.ExibirErro(ex.Message);
            }
            Console.WriteLine("\n\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
