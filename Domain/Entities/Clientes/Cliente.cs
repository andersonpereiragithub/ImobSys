using ImobSys.Domain.Enums;

namespace ImobSys.Domain.Entities.Clientes
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public Endereco Endereco { get; set; }
        public string Telefone {  get; set; }
        public List<TiposRelacao> TiposRelacoes { get; set; }

        public Cliente()
        {
            Id = Guid.NewGuid();
            TiposRelacoes = new List<TiposRelacao>();
        }
    }
}
