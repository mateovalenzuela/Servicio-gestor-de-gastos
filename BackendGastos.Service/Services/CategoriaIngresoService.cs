using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using BackendGastos.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendGastos.Service.Services
{
    public class CategoriaIngresoService : ICommonService<CategoriaIngresoDto, InsertUpdateCategoriaIngresoDto>
    {
        private CategoriaIngresoRepository _categoriaIngresoRepository;

        public CategoriaIngresoService(CategoriaIngresoRepository categoriaIngresoRepository)
        {
            _categoriaIngresoRepository = categoriaIngresoRepository;
        }

        public async Task<CategoriaIngresoDto> Add(InsertUpdateCategoriaIngresoDto insertUpadateCategoriaIngresoDto)
        {
            var categoriaIngreso = new GastosCategoriaigreso
            {
                Descripcion = insertUpadateCategoriaIngresoDto.Descripcion,
                FechaCreacion = DateTime.UtcNow
            };


            await _categoriaIngresoRepository.Add(categoriaIngreso);
            await _categoriaIngresoRepository.Save();


            var categoriaDto = new CategoriaIngresoDto()
            {
                Id = categoriaIngreso.Id,
                Descripcion = categoriaIngreso.Descripcion
            };
            return categoriaDto;
        }

        public async Task<CategoriaIngresoDto> Delete(long id)
        {
            var categoriaIngreso = await _categoriaIngresoRepository.GetActiveById(id);

            if (categoriaIngreso != null)
            {
                _categoriaIngresoRepository.BajaLogica(categoriaIngreso);
                await _categoriaIngresoRepository.Save();

                var categoriaIngresoDto = new CategoriaIngresoDto
                {
                    Id = categoriaIngreso.Id,
                    Descripcion = categoriaIngreso.Descripcion
                };
                return categoriaIngresoDto;
            }
            return null;
        }

        public async Task<IEnumerable<CategoriaIngresoDto>> Get()
        {
            var categoriaIngreso = await _categoriaIngresoRepository.GetActive();

            var categoriaIngresoDtos = categoriaIngreso.Select(c => new CategoriaIngresoDto
            {
                Id = c.Id,
                Descripcion = c.Descripcion,
            }).ToList();

            return categoriaIngresoDtos;
        }

        public async Task<CategoriaIngresoDto> GetById(long id)
        {
            var categoriaIngreso = await _categoriaIngresoRepository.GetActiveById(id);

            if (categoriaIngreso != null)
            {
                var categoriaIngresoDto = new CategoriaIngresoDto
                {
                    Id = categoriaIngreso.Id,
                    Descripcion = categoriaIngreso.Descripcion,
                };

                return categoriaIngresoDto;
            }
            return null;
        }


        public async Task<CategoriaIngresoDto> Update(long id, InsertUpdateCategoriaIngresoDto inserUpdateCategoriaIngresoDto)
        {
            var categoriaIngreso = await _categoriaIngresoRepository.GetActiveById(id);

            if (categoriaIngreso != null)
            {
                categoriaIngreso.Descripcion = inserUpdateCategoriaIngresoDto.Descripcion;
                await _categoriaIngresoRepository.Save();

                var categoriaDto = new CategoriaIngresoDto()
                {
                    Id = categoriaIngreso.Id,
                    Descripcion = categoriaIngreso.Descripcion
                };
            }

            return null;
        }


    }
}
