using ImobSys.Domain;

namespace ImobSys.Infrastructure.Repositories
{
    public interface IImovelRepository
    {
        void SalvarImovel(Imovel imovel);
        Imovel BuscarPorIdImovel(Guid id);
        List<Imovel> ListarTodosImovel();
        void RemoverImovel(Guid id);
    }
}
