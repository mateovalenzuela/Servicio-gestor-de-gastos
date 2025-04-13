using AutoMapper;
using BackendGastos.Repository.Models;
using BackendGastos.Repository.Repository;
using BackendGastos.Service.DTOs.CategoriaGasto;

namespace BackendGastos.Service.Services
{
    public class CategoriaGastoService : ICategoriaGastoService
    {
        private readonly ICategoriaGastoRepository _categoriaGastoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public Dictionary<string, string> Errors { get; }

        public CategoriaGastoService(ICategoriaGastoRepository categoriaGastoRepository, IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _categoriaGastoRepository = categoriaGastoRepository;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            Errors = new Dictionary<string, string>();
        }

        public async Task<CategoriaGastoDto> Add(InsertUpdateCategoriaGastoDto insertUpdateDto)
        {
            var categoriaGasto = _mapper.Map<GastosCategoriagasto>(insertUpdateDto);

            await _categoriaGastoRepository.Add(categoriaGasto);
            await _categoriaGastoRepository.Save();

            var categoriaGastoDto = _mapper.Map<CategoriaGastoDto>(categoriaGasto);
            return categoriaGastoDto;
        }

        public async Task<CategoriaGastoDto?> Delete(long id)
        {
            var categoriaGasto = await _categoriaGastoRepository.GetActiveById(id);

            if (categoriaGasto != null)
            {
                _categoriaGastoRepository.BajaLogica(categoriaGasto);
                await _categoriaGastoRepository.Save();

                var categoriaGastoDto = _mapper.Map<CategoriaGastoDto>(categoriaGasto);

                return categoriaGastoDto;
            }
            return null;
        }

        public async Task<IEnumerable<CategoriaGastoDto>> Get()
        {
            var categoriaGasto = await _categoriaGastoRepository.GetActive();

            var categoriaGastoDtos = categoriaGasto.Select(c => _mapper.Map<CategoriaGastoDto>(c)).ToList();
            return categoriaGastoDtos;
        }

        public async Task<CategoriaGastoDto?> GetById(long id)
        {
            var categoriaGasto = await _categoriaGastoRepository.GetActiveById(id);

            if (categoriaGasto != null)
            {
                var categoriaGastoDto = _mapper.Map<CategoriaGastoDto>(categoriaGasto);
                return categoriaGastoDto;
            }
            return null;
        }

        public async Task<CategoriaGastoDto?> Update(long id, InsertUpdateCategoriaGastoDto insertUpdateDto)
        {
            var categoriaGasto = await _categoriaGastoRepository.GetActiveById(id);

            if (categoriaGasto != null)
            {
                categoriaGasto.Descripcion = insertUpdateDto.Descripcion;
                await _categoriaGastoRepository.Save();

                var categoriaGastoDto = _mapper.Map<CategoriaGastoDto>(categoriaGasto);
                return categoriaGastoDto;
            }

            return null;
        }

        public bool Validate(InsertUpdateCategoriaGastoDto insertUpdateDto, long id)
        {
            if (_categoriaGastoRepository.Search(c => c.Descripcion == insertUpdateDto.Descripcion &&
                                                      id != c.Id).Count() > 0)
            {
                Errors.Add("Categoria", "La categoria ya existe");
                return false;
            }
            return true;
        }

        public bool Validate(InsertUpdateCategoriaGastoDto insertUpdateDto)
        {
            if (_categoriaGastoRepository.Search(c => c.Descripcion == insertUpdateDto.Descripcion).Count() > 0)
            {
                Errors.Add("Categoria", "La categoria ya existe");
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<CategoriaGastoWithAmountDto>> GetWithAmountDto(long idUser)
        {
            var userIsActive = await _usuarioRepository.IsActive(idUser);
            if (!userIsActive)
            {
                return null;
            }

            var categoriasGastoWithAmount = await _categoriaGastoRepository.GetActiveWithAmount(idUser);

            if (categoriasGastoWithAmount != null)
            {
                var categoriaGastoWithAmountDto = categoriasGastoWithAmount.Select(cA => _mapper.Map<CategoriaGastoWithAmountDto>(cA));
                return categoriaGastoWithAmountDto;
            }
            return null;
        }
    }
}
