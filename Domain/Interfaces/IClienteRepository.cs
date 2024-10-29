using ImobSys.Domain;
using ImobSys.Domain.Entities.Clientes;

namespace ImobSys.Domain.Interfaces
{
    public interface IClienteRepository
    {
        void SalvarCliente(Cliente cliente);
        Cliente BuscarPorIdCliente(Guid id);
        Cliente BuscarPorNomeCliente(string nome);
        List<Cliente> ListarTodosCliente();
        bool RemoverCliente(Guid id);
    }
}
