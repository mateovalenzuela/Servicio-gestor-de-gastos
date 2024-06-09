using AutoMapper;
using BackendGastos.Repository.Models;
using BackendGastos.Repository.Repository;
using BackendGastos.Service.DTOs.Ingreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public class IngresoService : IIngresoService
    {
        public Dictionary<string, string> Errors { get; }
        private readonly IIngresoRepository _ingresoRepository;
        private readonly IMapper _mapper;

        private readonly ISubCategoriaIngresoRepository _subCategoriaIngresoRepository;
        private readonly ICategoriaIngresoRepository _categoriaIngresoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMonedaRepository _monedaRepository;

        public IngresoService(IIngresoRepository ingresoRepository, 
            IMapper mapper, 
            ISubCategoriaIngresoRepository subCategoriaIngresoRepository, 
            ICategoriaIngresoRepository categoriaIngresoRepository, 
            IUsuarioRepository usuarioRepository, 
            IMonedaRepository monedaRepository)
        {
            Errors = [];
            _ingresoRepository = ingresoRepository;
            _mapper = mapper;
            _subCategoriaIngresoRepository = subCategoriaIngresoRepository;
            _categoriaIngresoRepository = categoriaIngresoRepository;
            _usuarioRepository = usuarioRepository;
            _monedaRepository = monedaRepository;
        }

        public async Task<IngresoDto> Add(InsertUpdateIngresoDto insertUpdateDto)
        {
            var ingreso = _mapper.Map<GastosIngreso>(insertUpdateDto);
            ingreso.FechaCreacion = DateTime.UtcNow;

            ingreso.CategoriaIngreso = await _categoriaIngresoRepository.GetActiveById(insertUpdateDto.CategoriaIngresoId);
            ingreso.SubcategoriaIngreso = await _subCategoriaIngresoRepository.GetActiveById(insertUpdateDto.SubcategoriaIngresoId);
            ingreso.Usuario = await _usuarioRepository.GetActiveById(insertUpdateDto.UsuarioId);
            ingreso.Moneda = await _monedaRepository.GetActiveById(insertUpdateDto.MonedaId);

            await _ingresoRepository.Add(ingreso);
            await _ingresoRepository.Save();

            var ingresoDto = _mapper.Map<IngresoDto>(ingreso);
            return ingresoDto;
        }

        public async Task<IngresoDto?> Delete(long id)
        {
            var ingreso = await _ingresoRepository.GetActiveById(id);

            if (ingreso != null)
            {
                _ingresoRepository.BajaLogica(ingreso);
                await _ingresoRepository.Save();

                var ingresoDto = _mapper.Map<IngresoDto>(ingreso);
                return ingresoDto;
            }
            return null;
        }

        public async Task<IEnumerable<IngresoDto>> Get()
        {
            var ingresos = await _ingresoRepository.GetActive();

            var ingresosDto = ingresos.Select(i => _mapper.Map<IngresoDto>(i)).ToList();

            return ingresosDto;
        }

        public async Task<IEnumerable<IngresoDto>> GetByCategoriaIngresoId(long idCategoriaIngreso)
        {
            var ingresos = await _ingresoRepository.GetActiveByCategoriaIngreso(idCategoriaIngreso);

            if (ingresos.Count() > 0)
            {
                var igresosDto = ingresos.Select(i => _mapper.Map<IngresoDto>(i)).ToList();
                return igresosDto;
            }

            return null;
        }

        public async Task<IngresoDto?> GetById(long id)
        {
            var ingreso = await _ingresoRepository.GetActiveById(id);

            if (ingreso != null)
            {
                var ingresoDto = _mapper.Map<IngresoDto?>(ingreso);
                return ingresoDto;
            }
            return null;
        }

        public async Task<IEnumerable<IngresoDto>> GetBySubCategoriaIngresoId(long idSubCategoriaIngreso)
        {
            var ingresos = await _ingresoRepository.GetActiveBySubCategoriaIngreso(idSubCategoriaIngreso);

            if (ingresos.Count() > 0)
            {
                var igresosDto = ingresos.Select(i => _mapper.Map<IngresoDto>(i)).ToList();
                return igresosDto;
            }

            return null;
        }

        public async Task<IEnumerable<IngresoDto>> GetByUserId(long idUser)
        {
            var ingresos = await _ingresoRepository.GetActiveByUser(idUser);

            if (ingresos.Count() > 0)
            {
                var igresosDto = ingresos.Select(i => _mapper.Map<IngresoDto>(i)).ToList();
                return igresosDto;
            }

            return null;
        }

        public async Task<IngresoDto?> Update(long id, InsertUpdateIngresoDto insertUpdateDto)
        {
            var ingreso = await _ingresoRepository.GetActiveById(id);

            if (ingreso != null)
            {
                ingreso.Descripcion = insertUpdateDto.Descripcion;
                ingreso.Importe = insertUpdateDto.Importe;
                ingreso.SubcategoriaIngresoId = insertUpdateDto.SubcategoriaIngresoId;
                ingreso.CategoriaIngresoId = insertUpdateDto.CategoriaIngresoId;
                ingreso.MonedaId = insertUpdateDto.MonedaId;

                await _ingresoRepository.Save();

                var ingresdoDto = _mapper.Map<IngresoDto>(ingreso);
                return ingresdoDto;
            }
            return null;
        }

        public async Task<bool> Validate(long id, InsertUpdateIngresoDto insertUpdateDto)
        {
            bool flag = true;

            var categoria = await _categoriaIngresoRepository.GetActiveById(insertUpdateDto.CategoriaIngresoId);
            if (categoria == null)
            {
                Errors.Add("Categoria", "La Categoria Ingreso no existe");
                flag = false;
            }

            var subcategoria = await _subCategoriaIngresoRepository.GetActiveById(insertUpdateDto.SubcategoriaIngresoId);
            if (subcategoria == null)
            {
                Errors.Add("Subcategoria", "La Subcategoria Ingreso no existe");
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

        public async Task<bool> Validate(InsertUpdateIngresoDto insertUpdateDto)
        {
            bool flag = true;

            var categoria = await _categoriaIngresoRepository.GetActiveById(insertUpdateDto.CategoriaIngresoId);
            if (categoria == null)
            {
                Errors.Add("Categoria", "La Categoria Ingreso no existe");
                flag = false;
            }

            var subcategoria = await _subCategoriaIngresoRepository.GetActiveById(insertUpdateDto.SubcategoriaIngresoId);
            if (subcategoria == null)
            {
                Errors.Add("Subcategoria", "La Subcategoria Ingreso no existe");
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

        public async Task<IEnumerable<IngresoDto>> GetByUserAndCategoriaIngreso(long idUser, long idCategoriaIngreso)
        {
            var ingresos = await _ingresoRepository.GetActiveByUserAndCategoriaIngreso(idUser, idCategoriaIngreso);

            if (ingresos.Count() > 0)
            {
                var ingresosDto = ingresos.Select(i => _mapper.Map<IngresoDto>(i)).ToList();
                return ingresosDto;
            }
            return null;
        }
    }
}
