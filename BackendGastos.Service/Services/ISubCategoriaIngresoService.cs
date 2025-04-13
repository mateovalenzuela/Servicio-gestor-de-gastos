using BackendGastos.Service.DTOs.SubCategoriaIngreso;

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

        Task<IEnumerable<CategoriaIngresoYSubCategoriasIngresoDto>> GetActiveGroupByCategoriaIngresoWithAmountByUser(long idUser);
    }
}
