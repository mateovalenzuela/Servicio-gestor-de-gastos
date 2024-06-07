using BackendGastos.Service.DTOs.SubCategoriaGasto;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public interface ISubCategoriaGastoService : ICommonService<SubCategoriaGastoDto, InsertUpdateSubCategoriaGastoDto>
    {
        Task<IEnumerable<SubCategoriaGastoDto>> GetActiveByUser(long idUser);

        Task<IEnumerable<SubCategoriaGastoDto>> GetActiveByUserAndCategoriaGasto(long idUser, long idSubCategoriaGasto);

        Task<IEnumerable<SubCategoriaGastoDto>> GetActiveByCategoriaGasto(long idSubCategoriaGasto);

        public Task<bool> Validate(InsertUpdateSubCategoriaGastoDto insertUpdateDto, long id);
        public Task<bool> Validate(InsertUpdateSubCategoriaGastoDto insertUpdateDto);
    }
}
