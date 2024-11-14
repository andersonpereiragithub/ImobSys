using ImobSys.Domain.Entities.Clientes;

namespace ImobSys.Domain.Interfaces
{
    public interface IClienteRepository<T> where T : Cliente
    {
        void SalvarCliente(T cliente);
        T BuscarPorIdCliente(Guid id);
        object BuscarPorNomeCliente(string nome);
        List<T> ListarTodosClientes();
        bool RemoverCliente(Guid id);
    }
}
