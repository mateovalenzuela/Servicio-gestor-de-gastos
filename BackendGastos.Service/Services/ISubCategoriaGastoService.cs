using BackendGastos.Service.DTOs.SubCategoriaGasto;

namespace BackendGastos.Service.Services
{
    public interface ISubCategoriaGastoService : ICommonService<SubCategoriaGastoDto, InsertUpdateSubCategoriaGastoDto>
    {
        Task<IEnumerable<SubCategoriaGastoDto>> GetActiveByUser(long idUser);

        Task<IEnumerable<SubCategoriaGastoDto>> GetActiveByUserAndCategoriaGasto(long idUser, long idSubCategoriaGasto);

        Task<IEnumerable<SubCategoriaGastoDto>> GetActiveByCategoriaGasto(long idSubCategoriaGasto);

        Task<IEnumerable<CategoriaGastoYSubCategoriasGastoDto>> GetActiveGroupByCategoriaGastoByUser(long idUser);

        Task<IEnumerable<CategoriaGastoYSubCategoriasGastoDto>> GetActiveGroupByCategoriaGastoWithAmountByUser(long idUser);

        public Task<bool> Validate(InsertUpdateSubCategoriaGastoDto insertUpdateDto, long id);
        public Task<bool> Validate(InsertUpdateSubCategoriaGastoDto insertUpdateDto);
    }
}
