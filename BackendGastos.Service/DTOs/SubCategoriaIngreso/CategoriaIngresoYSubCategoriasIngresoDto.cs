using BackendGastos.Service.DTOs.SubCategoriaGasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.DTOs.SubCategoriaIngreso
{
    public class CategoriaIngresoYSubCategoriasIngresoDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public virtual List<SubCategoriaIngresoDto> SubCategorias { get; set; } = new List<SubCategoriaIngresoDto>();
    }
}
