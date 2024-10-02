using BackendGastos.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Repository
{
    public interface IReporteRepository
    {
        Task<Dictionary<string, decimal>> GetImporteTotalDeGastosEIngresos(long idUser);

        Task<Dictionary<string, decimal>> GetImporteTotalDeGastosEIngresos(long idUser, DateTime fechaLimite, DateTime fechaInicial);

        Task<List<BalanceDiario>> GetBalanceDiarioPorUsuario(long idUser, DateTime fechaLimite, DateTime fechaInicial);

    }
}
