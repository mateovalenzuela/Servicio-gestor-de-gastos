using BackendGastos.Repository.Models;

namespace BackendGastos.Repository.Repository
{
    public interface ISubCategoriaIngresoRepository : IRepository<GastosSubcategoriaingreso>
    {

        Task<IEnumerable<GastosSubcategoriaingreso>> GetActiveByUser(long idUser);

        Task<IEnumerable<GastosSubcategoriaingreso>> GetActiveByUserAndCategoriaIngreso(long idUser, long idSubCategoriaIngreso);

        Task<IEnumerable<GastosSubcategoriaingreso>> GetActiveByCategoriaIngreso(long idSubCategoriaIngreso);

        IEnumerable<GastosSubcategoriaingreso> Search(Func<GastosSubcategoriaingreso, bool> filter);
    }
}
