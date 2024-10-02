using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.DTOs.Reporte
{
    public class BalanceDiarioDto
    {
        public DateTime Fecha {  get; set; }

        public decimal Importe {  get; set; }
    }
}
