using BackendGastos.Service.DTOs.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public interface ITransaccionService
    {
        Task<IEnumerable<TransaccionDto>> GetGastosEIngresos(long idUser, int cantidad);

        Task<ImporteTransaccionDto> GetImportes(long idUser);
    }
}
