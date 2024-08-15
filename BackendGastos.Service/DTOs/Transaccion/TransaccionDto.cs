using BackendGastos.Service.DTOs.Gasto;
using BackendGastos.Service.DTOs.Ingreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.DTOs.Transaccion
{
    public class TransaccionDto
    {
        public bool IsIngreso { get; set; }

        public GastoDto Gasto { get; set; }

        public IngresoDto Ingreso { get; set; }
    }
}
