namespace ImobSys.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? CPF { get; set; }
        public string? TipoCliente { get; set; }

        public Cliente()
        {
            Id = Guid.NewGuid();
        }
    }
}
