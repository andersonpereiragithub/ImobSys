using ImobSys.Domain;

namespace ImobSys.Infrastructure.Repositories
{
    public interface IImoveisRepository
    {
        void Salvar(Imovel imovel);
        Imovel BuscarPorId(Guid id);
        List<Imovel> ListarTodos();
        void Remover(Guid id);
    }
}
