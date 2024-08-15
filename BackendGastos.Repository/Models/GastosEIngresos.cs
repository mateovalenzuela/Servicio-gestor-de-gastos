using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Models
{
    public class GastosEIngresos
    {
        public bool IsIngreso {  get; set; }
        public GastosGasto? Gasto { get; set; }
        public GastosIngreso? Ingreso { get; set; }

    }
}
