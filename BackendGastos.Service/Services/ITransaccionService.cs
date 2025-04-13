using BackendGastos.Service.DTOs.Transaccion;

namespace BackendGastos.Service.Services
{
    public interface ITransaccionService
    {
        Task<IEnumerable<TransaccionDto>> GetGastosEIngresos(long idUser, int cantidad);

        Task<ImporteTransaccionDto> GetImportes(long idUser);
    }
}
