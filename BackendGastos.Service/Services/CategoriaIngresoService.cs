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
using AutoMapper;

namespace BackendGastos.Service.Services
{
    public class CategoriaIngresoService : ICommonService<CategoriaIngresoDto, InsertUpdateCategoriaIngresoDto>
    {
        private readonly ICategoriaIngresoRepository _categoriaIngresoRepository;
        private readonly IMapper _mapper;

        public CategoriaIngresoService(ICategoriaIngresoRepository categoriaIngresoRepository, IMapper mapper)
        {
            _categoriaIngresoRepository = categoriaIngresoRepository;
            _mapper = mapper;
        }

        public async Task<CategoriaIngresoDto> Add(InsertUpdateCategoriaIngresoDto insertUpadateCategoriaIngresoDto)
        {
            var categoriaIngreso = _mapper.Map<GastosCategoriaigreso>(insertUpadateCategoriaIngresoDto);
            categoriaIngreso.FechaCreacion = DateTime.UtcNow;


            await _categoriaIngresoRepository.Add(categoriaIngreso);
            await _categoriaIngresoRepository.Save();


            var categoriaDto = _mapper.Map<CategoriaIngresoDto>(categoriaIngreso);
            return categoriaDto;
        }

        public async Task<CategoriaIngresoDto> Delete(long id)
        {
            var categoriaIngreso = await _categoriaIngresoRepository.GetActiveById(id);

            if (categoriaIngreso != null)
            {
                _categoriaIngresoRepository.BajaLogica(categoriaIngreso);
                await _categoriaIngresoRepository.Save();

                var categoriaIngresoDto = _mapper.Map<CategoriaIngresoDto>(categoriaIngreso);
                return categoriaIngresoDto;
            }
            return null;
        }

        public async Task<IEnumerable<CategoriaIngresoDto>> Get()
        {
            var categoriaIngreso = await _categoriaIngresoRepository.GetActive();

            var categoriaIngresoDtos = categoriaIngreso.Select(c => _mapper.Map<CategoriaIngresoDto>(c)).ToList();

            return categoriaIngresoDtos;
        }

        public async Task<CategoriaIngresoDto> GetById(long id)
        {
            var categoriaIngreso = await _categoriaIngresoRepository.GetActiveById(id);

            if (categoriaIngreso != null)
            {
                var categoriaIngresoDto = _mapper.Map<CategoriaIngresoDto>(categoriaIngreso);

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

                var categoriaDto = _mapper.Map<CategoriaIngresoDto>(categoriaIngreso);
                return categoriaDto;
            }

            return null;
        }


    }
}
