using ImobSys.Domain;

namespace ImobSys.Domain.Interfaces
{
    public interface IImovelRepository
    {
        void SalvarImovel(Imovel imovel);
        Imovel BuscarPorIdImovel(Guid id);
        List<Imovel> ListarTodosImovel();
        void RemoverImovel(Guid id);
        bool ClientePossuiImovel(Guid clienteId);
        Imovel BuscarPorInscricaoIPTU(string? inscricaoIptu);
        List<Imovel> ObterImoveisPorCliente(Guid clienteId);
    }
}
