using BackendGastos.Service.DTOs.CategoriaGasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendGastos.Repository.Repository;
using BackendGastos.Repository.Models;
using AutoMapper;

namespace BackendGastos.Service.Services
{
    public class CategoriaGastoService : ICategoriaGastoService
    {
        private readonly ICategoriaGastoRepository _categoriaGastoRepository;
        private readonly IMapper _mapper;
        public List<string> Errors { get; }

        public CategoriaGastoService(ICategoriaGastoRepository categoriaGastoRepository, IMapper mapper)
        {
            _categoriaGastoRepository = categoriaGastoRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<CategoriaGastoDto> Add(InsertUpdateCategoriaGastoDto insertUpdateDto)
        {
            var categoriaGasto = _mapper.Map<GastosCategoriagasto>(insertUpdateDto);
            categoriaGasto.FechaCreacion = DateTime.UtcNow;

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

            var categoriaGastoDtos = categoriaGasto.Select(c=> _mapper.Map<CategoriaGastoDto>(c)).ToList();
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
                Errors.Add("La categoria ya existe");
                return false;
            }
            return true;
        }

        public bool Validate(InsertUpdateCategoriaGastoDto insertUpdateDto)
        {
            if (_categoriaGastoRepository.Search(c => c.Descripcion == insertUpdateDto.Descripcion).Count() > 0 )
            {
                Errors.Add("La categoria ya existe");
                return false;
            }
            return true;
        }
    }
}
