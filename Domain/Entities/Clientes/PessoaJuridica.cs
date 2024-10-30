using ImobSys.Domain.Enums;

namespace ImobSys.Domain.Entities.Clientes
{
    public class PessoaJuridica : Cliente
    {
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string NomeRepresentante { get; set; }
        public string InscricaoEstadual { get; set; }

        public PessoaJuridica(string razaoSocial, string cnpj, Endereco endereco, List<TiposRelacao> tiposRelacoes, string nomeRepresentante, string inscricaoEstadual)
        {
            RazaoSocial = razaoSocial ?? throw new ArgumentNullException(nameof(razaoSocial), "Razão Social não pode ser nula!");
            CNPJ = cnpj;
            Endereco = endereco;
            NomeRepresentante = nomeRepresentante;
            InscricaoEstadual = inscricaoEstadual;
            TiposRelacoes = tiposRelacoes ?? new List<TiposRelacao>();
        }
    }
}
