using BackendGastos.Controller.DTOs.CategoriaIngreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service
{
    internal interface ICategoriaIngresoService
    {
        Task<IEnumerable<CategoriaIngresoDto>> Get();
        Task<CategoriaIngresoDto> GetById(long id);
        Task<CategoriaIngresoDto> Add(InsertUpdateCategoriaIngresoDto categoriaIngresoDto);
        Task<CategoriaIngresoDto> Update(long id, InsertUpdateCategoriaIngresoDto categoriaIngresoDto);
        Task<CategoriaIngresoDto> Delete(long id);
    }
}
