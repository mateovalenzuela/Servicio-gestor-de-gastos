using AutoMapper;
using BackendGastos.Repository.Models;
using BackendGastos.Repository.Repository;
using BackendGastos.Service.DTOs.Gasto;

namespace BackendGastos.Service.Services
{
    public class GastoService : IGastoService
    {
        public Dictionary<string, string> Errors { get; }
        private readonly IGastoRepository _gastoRepository;
        private readonly IMapper _mapper;

        private readonly ISubCategoriaGastoRepository _subCategoriaRepository;
        private readonly ICategoriaGastoRepository _categoriaGastoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMonedaRepository _monedaRepository;

        public GastoService(IGastoRepository gastoRepository,
            IMapper mapper,
            ISubCategoriaGastoRepository subCategoriaRepository,
            ICategoriaGastoRepository categoriaGastoRepository,
            IUsuarioRepository usuarioRepository,
            IMonedaRepository monedaRepository)
        {
            Errors = [];
            _gastoRepository = gastoRepository;
            _mapper = mapper;
            _subCategoriaRepository = subCategoriaRepository;
            _categoriaGastoRepository = categoriaGastoRepository;
            _usuarioRepository = usuarioRepository;
            _monedaRepository = monedaRepository;
        }

        public async Task<GastoDto> Add(InsertUpdateGastoDto insertUpdateDto)
        {
            var gasto = _mapper.Map<GastosGasto>(insertUpdateDto);

            gasto.CategoriaGasto = await _categoriaGastoRepository.GetActiveById(insertUpdateDto.CategoriaGastoId);
            gasto.SubcategoriaGasto = await _subCategoriaRepository.GetActiveById(insertUpdateDto.SubcategoriaGastoId);
            gasto.Usuario = await _usuarioRepository.GetActiveById(insertUpdateDto.UsuarioId);
            gasto.Moneda = await _monedaRepository.GetById(insertUpdateDto.MonedaId);

            await _gastoRepository.Add(gasto);
            await _gastoRepository.Save();

            var gastoDto = _mapper.Map<GastoDto>(gasto);
            return gastoDto;
        }

        public async Task<GastoDto?> Delete(long id)
        {
            var gasto = await _gastoRepository.GetActiveById(id);

            if (gasto != null)
            {
                _gastoRepository.BajaLogica(gasto);
                await _gastoRepository.Save();

                var gastoDto = _mapper.Map<GastoDto>(gasto);
                return gastoDto;
            }
            return null;
        }

        public async Task<IEnumerable<GastoDto>> Get()
        {
            var gastos = await _gastoRepository.GetActive();

            var gastosDto = gastos.Select(g => _mapper.Map<GastoDto>(g)).ToList();
            return gastosDto;
        }

        public async Task<IEnumerable<GastoDto>> GetByCategoriaGastoId(long idCategoriaGasto)
        {
            var gastos = await _gastoRepository.GetActiveByCategoriaGasto(idCategoriaGasto);

            if (gastos.Count() > 0)
            {
                var gastosDto = gastos.Select(g => _mapper.Map<GastoDto>(g)).ToList();
                return gastosDto;
            }
            return null;
        }

        public async Task<GastoDto?> GetById(long id)
        {
            var gasto = await _gastoRepository.GetActiveById(id);

            if (gasto != null)
            {
                var gastoDto = _mapper.Map<GastoDto>(gasto);
                return gastoDto;
            }
            return null;
        }

        public async Task<IEnumerable<GastoDto>> GetBySubCategoriaGastoId(long idSubCategoriaGasto)
        {
            var gastos = await _gastoRepository.GetActiveBySubCategoriaGasto(idSubCategoriaGasto);

            if (gastos.Count() > 0)
            {
                var gastosDto = gastos.Select(g => _mapper.Map<GastoDto>(g)).ToList();
                return gastosDto;
            }
            return null;
        }

        public async Task<IEnumerable<GastoDto>> GetByUserAndCategoriaGasto(long idUser, long idCategoriaGasto)
        {
            var gastos = await _gastoRepository.GetActiveByUserAndCategoriaGasto(idUser, idCategoriaGasto);

            if (gastos.Count() > 0)
            {
                var gastosDto = gastos.Select(g => _mapper.Map<GastoDto>(g)).ToList();
                return gastosDto;
            }
            return null;
        }

        public async Task<IEnumerable<GastoDto>> GetByUserId(long idUser)
        {
            var gastos = await _gastoRepository.GetActiveByUser(idUser);

            if (gastos.Count() > 0)
            {
                var gastosDto = gastos.Select(g => _mapper.Map<GastoDto>(g)).ToList();
                return gastosDto;
            }
            return null;
        }

        public async Task<GastoDto?> Update(long id, InsertUpdateGastoDto insertUpdateDto)
        {
            var gasto = await _gastoRepository.GetActiveById(id);

            if (gasto != null)
            {
                gasto.Descripcion = insertUpdateDto.Descripcion;
                gasto.Importe = insertUpdateDto.Importe;
                gasto.SubcategoriaGastoId = insertUpdateDto.SubcategoriaGastoId;
                gasto.CategoriaGastoId = insertUpdateDto.CategoriaGastoId;
                gasto.MonedaId = insertUpdateDto.MonedaId;

                await _gastoRepository.Save();

                var gastoDto = _mapper.Map<GastoDto>(gasto);
                return gastoDto;
            }
            return null;
        }

        public async Task<bool> Validate(long id, InsertUpdateGastoDto insertUpdateDto)
        {
            bool flag = true;

            var categoria = await _categoriaGastoRepository.GetActiveById(insertUpdateDto.CategoriaGastoId);
            if (categoria == null)
            {
                Errors.Add("Categoria", "La Categoria Gasto no existe");
                flag = false;
            }

            var subcategoria = await _subCategoriaRepository.GetActiveById(insertUpdateDto.SubcategoriaGastoId);
            if (subcategoria == null)
            {
                Errors.Add("Subcategoria", "La Subcategoria Gasto no existe");
                flag = false;
            }

            var user = await _usuarioRepository.GetActiveById(insertUpdateDto.UsuarioId);
            if (user == null)
            {
                Errors.Add("Usuario", "El Usuario no existe");
                flag = false;
            }

            var moneda = await _monedaRepository.GetActiveById(insertUpdateDto.MonedaId);
            if (moneda == null)
            {
                Errors.Add("Moneda", "La Moneda no es valida");
                flag = false;
            }

            return flag;
        }

        public async Task<bool> Validate(InsertUpdateGastoDto insertUpdateDto)
        {
            bool flag = true;

            var categoria = await _categoriaGastoRepository.GetActiveById(insertUpdateDto.CategoriaGastoId);
            if (categoria == null)
            {
                Errors.Add("Categoria", "La Categoria Gasto no existe");
                flag = false;
            }

            var subcategoria = await _subCategoriaRepository.GetActiveById(insertUpdateDto.SubcategoriaGastoId);
            if (subcategoria == null)
            {
                Errors.Add("Subcategoria", "La Subcategoria Gasto no existe");
                flag = false;
            }

            var user = await _usuarioRepository.GetActiveById(insertUpdateDto.UsuarioId);
            if (user == null)
            {
                Errors.Add("Usuario", "El Usuario no existe");
                flag = false;
            }

            var moneda = await _monedaRepository.GetActiveById(insertUpdateDto.MonedaId);
            if (moneda == null)
            {
                Errors.Add("Moneda", "La Moneda no es valida");
                flag = false;
            }

            return flag;
        }

        public async Task<IEnumerable<GastoDto>> SearchByDescripcionParcial(long idUser, string descripcion)
        {
            var userIsActive = await _usuarioRepository.IsActive(idUser);

            if (!userIsActive)
            {
                return null;
            }
            var limit = 5;
            var gastos = await _gastoRepository.SearchActiveByDescripcionParcial(idUser, descripcion, limit);

            var gastosDto = gastos.Select(g => _mapper.Map<GastoDto>(g));
            return gastosDto;
        }
    }
}
