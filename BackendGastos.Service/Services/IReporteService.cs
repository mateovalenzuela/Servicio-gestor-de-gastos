using BackendGastos.Service.DTOs.Reporte;

namespace BackendGastos.Service.Services
{
    public interface IReporteService
    {
        Task<ImportesDto> GetImporteTotalDeGastosEIngresos(long idUser);

        Task<ImportesDto> GetImporteTotalDeGastosEIngresos(long idUser, DateTime fechaLimite, DateTime fechaInicial);

        Task<List<BalanceDiarioDto>> GetBalanceDiarioPorUsuario(long idUser, DateTime fechaLimite, DateTime fechaInicial);

    }
}
