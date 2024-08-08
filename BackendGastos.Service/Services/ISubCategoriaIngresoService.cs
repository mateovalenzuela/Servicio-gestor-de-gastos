using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.SubCategoriaGasto;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public interface ISubCategoriaIngresoService : ICommonService<SubCategoriaIngresoDto, InsertUpdateSubCategoriaIngresoDto>
    {
        Task<IEnumerable<SubCategoriaIngresoDto>> GetActiveByUser(long idUser);

        Task<IEnumerable<SubCategoriaIngresoDto>> GetActiveByUserAndCategoriaIngreso(long idUser, long idSubCategoriaIngreso);

        Task<IEnumerable<SubCategoriaIngresoDto>> GetActiveByCategoriaIngreso(long idSubCategoriaIngreso);

        Task<IEnumerable<CategoriaIngresoYSubCategoriasIngresoDto>> GetActiveGroupByCategoriaIngresoByUser(long idUser);

        public Task<bool> Validate(InsertUpdateSubCategoriaIngresoDto insertUpdateDto, long id);
        public Task<bool> Validate(InsertUpdateSubCategoriaIngresoDto insertUpdateDto);
    }
}
