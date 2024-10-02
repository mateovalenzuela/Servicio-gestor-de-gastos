using BackendGastos.Repository.Repository;
using BackendGastos.Service.DTOs.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public interface IReporteService
    {
        Task<ImportesDto> GetImporteTotalDeGastosEIngresos(long idUser);

        Task<ImportesDto> GetImporteTotalDeGastosEIngresos(long idUser, DateTime fechaLimite, DateTime fechaInicial);

        Task<List<BalanceDiarioDto>> GetBalanceDiarioPorUsuario(long idUser, DateTime fechaLimite, DateTime fechaInicial);

    }
}
