using ImobSys.Domain.Entities.Clientes;
using ImobSys.Domain.Enums;

namespace ImobSys.Domain.Entities.Clientes
{
    public class PessoaFisica : Cliente
    {
        public string CPF { get; set; }

        public PessoaFisica(string nome, string cpf, Endereco endereco, List<TiposRelacao> tiposRelacoes)
        {
            Nome = nome;
            CPF = cpf;
            Endereco = endereco;
            TiposRelacoes = tiposRelacoes ?? new List<TiposRelacao>();
        }
    }
}
