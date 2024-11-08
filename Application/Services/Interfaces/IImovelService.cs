using System.Collections.Generic;
using ImobSys.Domain;
using ImobSys.Domain.Entities.Clientes;

namespace ImobSys.Application.Services.Interfaces
{
    public interface IImovelService
    {
        List<Imovel> ListarTodosImoveis();
        public void CadastrarNovoImovel();
    }
}
