using BackendGastos.Repository.Models;

namespace BackendGastos.Repository.Repository
{
    public interface ICategoriaGastoRepository : IRepository<GastosCategoriagasto>
    {
        IEnumerable<GastosCategoriagasto> Search(Func<GastosCategoriagasto, bool> filter);

        Task<IEnumerable<CategoriagastoAmount>> GetActiveWithAmount(long idUser);
    }
}
