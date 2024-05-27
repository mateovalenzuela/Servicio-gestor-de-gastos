using BackendGastos.Service.DTOs.CategoriaIngreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public interface ICategoriaIngresoService : ICommonService<CategoriaIngresoDto, InsertUpdateCategoriaIngresoDto>
    {
        public bool Validate(InsertUpdateCategoriaIngresoDto insertUpdateDto, long id);
        public bool Validate(InsertUpdateCategoriaIngresoDto insertUpdateDto);
    }
}
