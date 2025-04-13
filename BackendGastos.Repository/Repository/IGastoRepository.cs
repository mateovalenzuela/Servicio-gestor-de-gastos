using BackendGastos.Repository.Models;

namespace BackendGastos.Repository.Repository
{
    public interface IGastoRepository : IRepository<GastosGasto>
    {
        Task<IEnumerable<GastosGasto>> GetActiveByUser(long idUser);

        Task<IEnumerable<GastosGasto>> GetActiveByCategoriaGasto(long idCategoriaGasto);

        Task<IEnumerable<GastosGasto>> GetActiveBySubCategoriaGasto(long idSubCategoriaGasto);

        Task<IEnumerable<GastosGasto>> GetActiveByUserAndCategoriaGasto(long idUser, long idCategoriaGasto);

        IEnumerable<GastosGasto> Search(Func<GastosGasto, bool> filter, int limit);

        Task<IEnumerable<GastosGasto>> SearchActiveByDescripcionParcial(long idUser, string descripcion, int limit);
    }
}
