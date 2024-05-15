using BackendGastos.Controller.DTOs.CategoriaIngreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service
{
    internal class CategoriaIngresoService : ICategoriaIngresoService
    {
        public Task<CategoriaIngresoDto> Add(InsertUpdateCategoriaIngresoDto categoriaIngresoDto)
        {
            throw new NotImplementedException();
        }

        public Task<CategoriaIngresoDto> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoriaIngresoDto>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<CategoriaIngresoDto> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoriaIngresoDto> Update(long id, InsertUpdateCategoriaIngresoDto categoriaIngresoDto)
        {
            throw new NotImplementedException();
        }
    }
}
