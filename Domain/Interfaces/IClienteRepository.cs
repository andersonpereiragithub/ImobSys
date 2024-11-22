using ImobSys.Domain.Entities.Clientes;

namespace ImobSys.Domain.Interfaces
{
    public interface IClienteRepository<T> where T : Cliente
    {
        void SalvarCliente(T cliente);
        T BuscarPorIdCliente(Guid id);
        Guid ObterClientePorNome(string nome);
        List<T> ListarTodosClientes();
        bool RemoverCliente(Guid id);
    }
}
