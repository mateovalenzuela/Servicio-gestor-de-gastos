using BackendGastos.Service.DTOs.CategoriaIngreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public interface ICommonService<TDto, InsertUpdateTDto>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<TDto>> Get();
        Task<TDto?> GetById(long id);
        Task<TDto> Add(InsertUpdateTDto insertUpdateDto);
        Task<TDto?> Update(long id, InsertUpdateTDto insertUpdateDto);
        Task<TDto?> Delete(long id);
        public bool Validate(InsertUpdateTDto insertUpdateDto, long id);
        public bool Validate(InsertUpdateTDto insertUpdateDto);
    }
}
