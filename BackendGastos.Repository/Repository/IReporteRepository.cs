using BackendGastos.Repository.Models;

namespace BackendGastos.Repository.Repository
{
    public interface IReporteRepository
    {
        Task<Dictionary<string, decimal>> GetImporteTotalDeGastosEIngresos(long idUser);

        Task<Dictionary<string, decimal>> GetImporteTotalDeGastosEIngresos(long idUser, DateTime fechaLimite, DateTime fechaInicial);

        Task<List<BalanceDiario>> GetBalanceDiarioPorUsuario(long idUser, DateTime fechaLimite, DateTime fechaInicial);

    }
}
