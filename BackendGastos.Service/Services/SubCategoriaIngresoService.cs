using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendGastos.Repository.Repository;
using AutoMapper;
using BackendGastos.Repository.Models;

namespace BackendGastos.Service.Services
{
    public class SubCategoriaIngresoService : ISubCategoriaIngresoService
    {
        public List<string> Errors { get; }
        private readonly ISubCategoriaIngresoRepository _subCategoriaIngresoRepository;
        private readonly IMapper _mapper;

        public SubCategoriaIngresoService(ISubCategoriaIngresoRepository subCategoriaIngresoRepository, IMapper mapper)
        {
            Errors = new List<string>();
            _subCategoriaIngresoRepository = subCategoriaIngresoRepository;
            _mapper = mapper;
        }

        public async Task<SubCategoriaIngresoDto> Add(InsertUpdateSubCategoriaIngresoDto insertUpdateDto)
        {
            var subCategoriaIngreso = _mapper.Map<GastosSubcategoriaingreso>(insertUpdateDto);
            subCategoriaIngreso.FechaCreacion = DateTime.UtcNow;

            subCategoriaIngreso.CategoriaIngreso = await _subCategoriaIngresoRepository.GetCategoriaIngresoById(insertUpdateDto.CategoriaIngresoId);
            subCategoriaIngreso.Usuario = await _subCategoriaIngresoRepository.GetUsuarioById(insertUpdateDto.UsuarioId);

            await _subCategoriaIngresoRepository.Add(subCategoriaIngreso);
            await _subCategoriaIngresoRepository.Save();

            var subCategoriaIngresoDto = _mapper.Map<SubCategoriaIngresoDto>(subCategoriaIngreso);
            return subCategoriaIngresoDto;
        }

        public async Task<SubCategoriaIngresoDto?> Delete(long id)
        {
            var subCategoriaIngreso = await _subCategoriaIngresoRepository.GetActiveById(id);

            if (subCategoriaIngreso != null)
            {
                _subCategoriaIngresoRepository.BajaLogica(subCategoriaIngreso);
                await _subCategoriaIngresoRepository.Save();

                var subCategoriaIngresoDto = _mapper.Map<SubCategoriaIngresoDto>(subCategoriaIngreso);
                return subCategoriaIngresoDto;
            }

            return null;
        }

        public async Task<IEnumerable<SubCategoriaIngresoDto>> Get()
        {
            var subCategoriasIngreso = await _subCategoriaIngresoRepository.GetActive();

            var subCategoriasIngresoDto = subCategoriasIngreso.Select(c => _mapper.Map<SubCategoriaIngresoDto>(c)).ToList();

            return subCategoriasIngresoDto;
        }

        public async Task<SubCategoriaIngresoDto?> GetById(long id)
        {
            var subCategoriaIngreso = await _subCategoriaIngresoRepository.GetActiveById(id);

            if ( subCategoriaIngreso != null)
            {
                var subCategoriaIngresoDto = _mapper.Map<SubCategoriaIngresoDto?>(subCategoriaIngreso);
                return subCategoriaIngresoDto;
            }

            return null;
        }

        public async Task<IEnumerable<SubCategoriaIngresoDto>> GetActiveByUser(long idUser)
        {
            var subCategoriasIngresoList = await _subCategoriaIngresoRepository.GetActiveByUser(idUser);

            if (subCategoriasIngresoList.Count() > 0)
            {
                var subCategoriasIngresoDto = subCategoriasIngresoList.Select(c => _mapper.Map<SubCategoriaIngresoDto>(c)).ToList();
                return subCategoriasIngresoDto;
            }

            return null;
        }

        public async Task<IEnumerable<SubCategoriaIngresoDto>> GetActiveByCategoriaIngreso(long idCategoriaIngreso)
        {
            var subCategoriasIngresoList = await _subCategoriaIngresoRepository.GetActiveByCategoriaIngreso(idCategoriaIngreso);

            if (subCategoriasIngresoList.Count() > 0)
            {
                var subCategoriasIngresoDto = subCategoriasIngresoList.Select(c => _mapper.Map<SubCategoriaIngresoDto>(c)).ToList();
                return subCategoriasIngresoDto;
            }

            return null;
        }

        public async Task<IEnumerable<SubCategoriaIngresoDto>> GetActiveByUserAndCategoriaIngreso(long idUser, long idCategoriaIngreso)
        {
            var subCategoriasIngresoList = await _subCategoriaIngresoRepository.GetActiveByUserAndCategoriaIngreso(idUser, idCategoriaIngreso);

            if (subCategoriasIngresoList.Count() > 0)
            {
                var subCategoriasIngresoDto = subCategoriasIngresoList.Select(c => _mapper.Map<SubCategoriaIngresoDto>(c)).ToList();
                return subCategoriasIngresoDto;
            }

            return null;
        }

        public async Task<SubCategoriaIngresoDto?> Update(long id, InsertUpdateSubCategoriaIngresoDto insertUpdateDto)
        {
            var subCategoriaIngreso = await _subCategoriaIngresoRepository.GetActiveById(id);

            if (subCategoriaIngreso != null)
            {
                subCategoriaIngreso.Descripcion = insertUpdateDto.Descripcion;
                subCategoriaIngreso.CategoriaIngresoId = insertUpdateDto.CategoriaIngresoId;
                await _subCategoriaIngresoRepository.Save();

                var subCategoriaIngresoDto = _mapper.Map<SubCategoriaIngresoDto?>(subCategoriaIngreso);
                return subCategoriaIngresoDto;
            }

            return null;
        }

        public async Task<bool> Validate(InsertUpdateSubCategoriaIngresoDto insertUpdateDto, long id)
        {
            bool flag = true;
            if (_subCategoriaIngresoRepository.Search(s => s.Descripcion == insertUpdateDto.Descripcion &&
                                                            id != s.Id).Count() > 0)
            {
                Errors.Add("La SubCategoria ya existe");
                flag = false;
            }

            var categoria = await _subCategoriaIngresoRepository.GetCategoriaIngresoById(insertUpdateDto.CategoriaIngresoId);
            if (categoria == null)
            {
                Errors.Add("La Categoria Ingreso no existe");
                flag = false;
            }

            var user = await _subCategoriaIngresoRepository.GetUsuarioById(insertUpdateDto.UsuarioId);
            if (user == null)
            {
                Errors.Add("El Usuario no existe");
                flag = false;
            }

            return flag;
        }

        public async Task<bool> Validate(InsertUpdateSubCategoriaIngresoDto insertUpdateDto)
        {
            bool flag = true;
            if (_subCategoriaIngresoRepository.Search(s => s.Descripcion == insertUpdateDto.Descripcion).Count() > 0)
            {
                Errors.Add("La SubCategoria ya existe");
                flag = false;
            }

            var categoria = await _subCategoriaIngresoRepository.GetCategoriaIngresoById(insertUpdateDto.CategoriaIngresoId);
            if (categoria == null)
            {
                Errors.Add("La Categoria Ingreso no existe");
                flag = false;
            }

            var user = await _subCategoriaIngresoRepository.GetUsuarioById(insertUpdateDto.UsuarioId);
            if (user == null)
            {
                Errors.Add("El Usuario no existe");
                flag = false;
            }

            return flag;
        }

   
    }
}
