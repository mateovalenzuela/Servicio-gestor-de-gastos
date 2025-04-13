using BackendGastos.Repository.Models;

namespace BackendGastos.Repository.Repository
{
    public interface ITransaccionRepository
    {
        Task<IEnumerable<GastosEIngresos>> GetActiveGastosEIngresos(long idUser, int cantidad);

        Task<Dictionary<string, decimal>> GetImportesGastosEIngresos(long idUser);
    }
}
