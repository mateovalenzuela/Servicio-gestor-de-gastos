using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public interface IUtilidadesService
    {
        Task<DateTime?> GetDateTimeNow();

        Task<DateTime?> ValidateDateTime(string fecha);
    }
}
