using BackendGastos.Service.DTOs.CategoriaGasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendGastos.Repository.Repository;
using BackendGastos.Repository.Models;

namespace BackendGastos.Service.Services
{
    public class CategoriaGastoService : ICommonService<CategoriaGastoDto, InsertUpdateCategoriaGastoDto>
    {
        private readonly ICategoriaGastoRepository _categoriaGastoRepository;

        public CategoriaGastoService(ICategoriaGastoRepository categoriaGastoRepository)
        {
            _categoriaGastoRepository = categoriaGastoRepository;
        }

        public async Task<CategoriaGastoDto> Add(InsertUpdateCategoriaGastoDto insertUpdateDto)
        {
            var categoriaGasto = new GastosCategoriagasto
            {
                Descripcion = insertUpdateDto.Descripcion,
                FechaCreacion = DateTime.UtcNow
            };

            await _categoriaGastoRepository.Add(categoriaGasto);
            await _categoriaGastoRepository.Save();

            var categoriaGastoDto = new CategoriaGastoDto
            {
                Id = categoriaGasto.Id,
                Descripcion = categoriaGasto.Descripcion
            };
            return categoriaGastoDto;
        }

        public async Task<CategoriaGastoDto> Delete(long id)
        {
            var categoriaGasto = await _categoriaGastoRepository.GetActiveById(id);

            if (categoriaGasto != null)
            {
                _categoriaGastoRepository.BajaLogica(categoriaGasto);
                await _categoriaGastoRepository.Save();

                var categoriaGastoDto = new CategoriaGastoDto
                {
                    Id = categoriaGasto.Id,
                    Descripcion = categoriaGasto.Descripcion
                };

                return categoriaGastoDto;
            }
            return null;
        }

        public async Task<IEnumerable<CategoriaGastoDto>> Get()
        {
            var categoriaGasto = await _categoriaGastoRepository.GetActive();

            var categoriaGastoDtos = categoriaGasto.Select(c=> new CategoriaGastoDto
            {
                Id=c.Id,
                Descripcion=c.Descripcion,
            }).ToList();
            return categoriaGastoDtos;
        }

        public async Task<CategoriaGastoDto> GetById(long id)
        {
            var categoriaGasto = await _categoriaGastoRepository.GetActiveById(id);

            if (categoriaGasto != null)
            {
                var categoriaGastoDto = new CategoriaGastoDto
                {
                    Id = categoriaGasto.Id,
                    Descripcion = categoriaGasto.Descripcion
                };
                return categoriaGastoDto;
            }
            return null;
        }

        public async Task<CategoriaGastoDto> Update(long id, InsertUpdateCategoriaGastoDto insertUpdateDto)
        {
            var categoriaGasto = await _categoriaGastoRepository.GetActiveById(id);

            if (categoriaGasto != null)
            {
                categoriaGasto.Descripcion = insertUpdateDto.Descripcion;
                await _categoriaGastoRepository.Save();

                var categoriaGastoDto = new CategoriaGastoDto
                {
                    Id = categoriaGasto.Id,
                    Descripcion = categoriaGasto.Descripcion
                };
                return categoriaGastoDto;
            }

            return null;
        }
    }
}
