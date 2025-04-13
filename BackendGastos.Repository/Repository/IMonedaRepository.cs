using BackendGastos.Repository.Models;

namespace BackendGastos.Repository.Repository
{
    public interface IMonedaRepository : IRepository<GastosMonedum>
    {
        IEnumerable<GastosMonedum> Search(Func<GastosMonedum, bool> filter);

    }
}
