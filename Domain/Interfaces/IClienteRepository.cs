using ImobSys.Domain;
using ImobSys.Domain.Entities.Clientes;

namespace ImobSys.Domain.Interfaces
{
    public interface IClienteRepository<T> where T : Cliente
    {
        void SalvarCliente(T cliente);
        T BuscarPorIdCliente(Guid id);
        T BuscarPorNomeCliente(string nome);
        List<T> ListarTodosCliente();
        bool RemoverCliente(Guid id);
    }
}
