using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.Gasto;
using BackendGastos.Service.DTOs.Ingreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public interface IGastoService : ICommonService<GastoDto, InsertUpdateGastoDto>
    {
        Task<IEnumerable<GastoDto>> GetByUserId(long idUser);
        Task<IEnumerable<GastoDto>> GetByCategoriaGastoId(long idCategoriaGasto);
        Task<IEnumerable<GastoDto>> GetBySubCategoriaGastoId(long idSubCategoriaGasto);
        Task<IEnumerable<GastoDto>> GetByUserAndCategoriaGasto(long idUser, long idCategoriaGasto);
        Task<bool> Validate(long id, InsertUpdateGastoDto insertUpdateDto);
        Task<bool> Validate(InsertUpdateGastoDto insertUpdateDto);

        Task<IEnumerable<GastoDto>> SearchByDescripcionParcial(long idUser, string descripcion);
    }
}
