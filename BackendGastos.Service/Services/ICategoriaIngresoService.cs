using BackendGastos.Service.DTOs.CategoriaIngreso;

namespace BackendGastos.Service.Services
{
    public interface ICategoriaIngresoService : ICommonService<CategoriaIngresoDto, InsertUpdateCategoriaIngresoDto>
    {
        public bool Validate(InsertUpdateCategoriaIngresoDto insertUpdateDto, long id);
        public bool Validate(InsertUpdateCategoriaIngresoDto insertUpdateDto);

        public Task<IEnumerable<CategoriaIngresoWithAmountDto>> GetWithAmountDto(long idUser);

    }
}
