using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Models
{
    public class BalanceDiario
    {
        public DateTime Fecha { get; set; }
        public decimal Balance { get; set; }
    }

}
