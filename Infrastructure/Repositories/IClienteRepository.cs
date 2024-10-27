using ImobSys.Domain.Entities;
using ImobSys.Domain;

namespace ImobSys.Infrastructure.Repositories
{
    public interface IClienteRepository
    {
        void SalvarCliente(Cliente cliente);
        Cliente BuscarPorIdCliente(Guid id);
        List<Cliente> ListarTodosCliente();
        bool RemoverCliente(Guid id);
    }
}
