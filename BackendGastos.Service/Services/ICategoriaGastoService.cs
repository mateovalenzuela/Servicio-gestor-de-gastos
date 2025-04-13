using BackendGastos.Service.DTOs.CategoriaGasto;

namespace BackendGastos.Service.Services
{
    public interface ICategoriaGastoService : ICommonService<CategoriaGastoDto, InsertUpdateCategoriaGastoDto>
    {
        public bool Validate(InsertUpdateCategoriaGastoDto insertUpdateDto, long id);
        public bool Validate(InsertUpdateCategoriaGastoDto insertUpdateDto);

        public Task<IEnumerable<CategoriaGastoWithAmountDto>> GetWithAmountDto(long idUser);
    }
}
