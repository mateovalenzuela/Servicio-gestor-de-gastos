using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.DTOs.Reporte
{
    public class ObtenerReporteDto
    {
        public DateTime FechaLimite { get; set; }

        public DateTime FechaInicial { get; set; }
    }
}
