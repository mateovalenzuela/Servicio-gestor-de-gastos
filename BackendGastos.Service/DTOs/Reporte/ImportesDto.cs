using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.DTOs.Reporte
{
    public class ImportesDto
    {
        public decimal ImporteIngresos {  get; set; }
        public decimal ImporteGastos {  get; set; }
        public decimal ImportesTotales {  get; set; }
    }
}
