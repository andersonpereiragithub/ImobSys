using System.Collections.Generic;
using ImobSys.Domain;
using ImobSys.Domain.Entities.Clientes;

namespace ImobSys.Application.Services.Interfaces
{
    public interface IClienteService
    {
        public (object cliente, List<Imovel> imoveis) ObterClienteESeusImoveis(string nomeCliente);
        List<Cliente> ListarTodosClientes();
        public void CadastrarNovoCliente();
        void RemoverCliente();
    }
}
