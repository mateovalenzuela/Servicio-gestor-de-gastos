using BackendGastos.Service.DTOs.CategoriaGasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public interface ICategoriaGastoService : ICommonService<CategoriaGastoDto, InsertUpdateCategoriaGastoDto>
    {
        public bool Validate(InsertUpdateCategoriaGastoDto insertUpdateDto, long id);
        public bool Validate(InsertUpdateCategoriaGastoDto insertUpdateDto);

        public Task<IEnumerable<CategoriaGastoWithAmountDto>> GetWithAmountDto(long idUser);
    }
}
