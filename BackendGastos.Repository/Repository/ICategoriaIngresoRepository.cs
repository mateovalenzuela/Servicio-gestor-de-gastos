using BackendGastos.Repository.Models;

namespace BackendGastos.Repository.Repository
{
    public interface ICategoriaIngresoRepository : IRepository<GastosCategoriaigreso>
    {
        IEnumerable<GastosCategoriaigreso> Search(Func<GastosCategoriaigreso, bool> filter);

        Task<IEnumerable<CategoriaingresoAmount>> GetActiveWithAmount(long idUser);

    }
}
