using BackendGastos.Service.DTOs.Ingreso;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public interface IIngresoService : ICommonService<IngresoDto, InsertUpdateIngresoDto>
    {
        Task<IEnumerable<IngresoDto>> GetByUserId(long idUser);
        Task<IEnumerable<IngresoDto>> GetByCategoriaIngresoId(long idCategoriaIngreso);
        Task<IEnumerable<IngresoDto>> GetBySubCategoriaIngresoId(long idSubCategoriaIngreso);
        Task<IEnumerable<IngresoDto>> GetByUserAndCategoriaIngreso(long idUser, long idCategoriaIngreso);
        Task<bool> Validate(long id, InsertUpdateIngresoDto insertUpdateDto);
        Task<bool> Validate(InsertUpdateIngresoDto insertUpdateDto);

    }
}
