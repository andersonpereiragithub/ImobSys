using System.Collections.Generic;
using ImobSys.Domain.Entities.Clientes;

namespace ImobSys.Application.Services.Interfaces
{
    public interface IClienteService
    {
        List<Cliente> ListarTodosClientes();
        public void CadastrarNovoCliente();
    }
}
