using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Repository.Models
{
    public class CategoriagastoAmount
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public decimal ImporteTotal { get; set; }
    }
}
