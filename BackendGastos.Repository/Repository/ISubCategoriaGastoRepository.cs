using BackendGastos.Repository.Models;

namespace BackendGastos.Repository.Repository
{
    public interface ISubCategoriaGastoRepository : IRepository<GastosSubcategoriagasto>
    {
        Task<IEnumerable<GastosSubcategoriagasto>> GetActiveByUser(long idUser);

        Task<IEnumerable<GastosSubcategoriagasto>> GetActiveByUserAndCategoriaGasto(long idUser, long idCategoriaGasto);

        Task<IEnumerable<GastosSubcategoriagasto>> GetActiveByCategoriaGasto(long idCategoriaGasto);

        IEnumerable<GastosSubcategoriagasto> Search(Func<GastosSubcategoriagasto, bool> filter);

    }
}
