using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.DTOs.Transaccion
{
    public class ImporteTransaccionDto
    {
        public decimal ImporteTotal { get; set; }

        public decimal ImporteIngresos { get; set; }

        public decimal ImporteGastos { get; set; }
    }
}
