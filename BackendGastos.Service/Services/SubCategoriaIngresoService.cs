using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendGastos.Repository.Repository;
using AutoMapper;
using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.SubCategoriaGasto;

namespace BackendGastos.Service.Services
{
    public class SubCategoriaIngresoService : ISubCategoriaIngresoService
    {
        public Dictionary<string, string> Errors { get; }
        private readonly ISubCategoriaIngresoRepository _subCategoriaIngresoRepository;
        private readonly IMapper _mapper;

        private readonly ICategoriaIngresoRepository _categoriaIngresoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SubCategoriaIngresoService(ISubCategoriaIngresoRepository subCategoriaIngresoRepository, 
            IMapper mapper, 
            ICategoriaIngresoRepository categoriaIngresoRepository, 
            IUsuarioRepository usuarioRepository)
        {
            Errors = [];
            _subCategoriaIngresoRepository = subCategoriaIngresoRepository;
            _mapper = mapper;
            _categoriaIngresoRepository = categoriaIngresoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<SubCategoriaIngresoDto> Add(InsertUpdateSubCategoriaIngresoDto insertUpdateDto)
        {
            var subCategoriaIngreso = _mapper.Map<GastosSubcategoriaingreso>(insertUpdateDto);
            subCategoriaIngreso.FechaCreacion = DateTime.UtcNow;

            subCategoriaIngreso.CategoriaIngreso = await _categoriaIngresoRepository.GetActiveById(insertUpdateDto.CategoriaIngresoId);
            subCategoriaIngreso.Usuario = await _usuarioRepository.GetActiveById(insertUpdateDto.UsuarioId);

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

        public async Task<IEnumerable<CategoriaIngresoYSubCategoriasIngresoDto>> GetActiveGroupByCategoriaIngresoByUser(long idUser)
        {
            var categoriasIngresoList = await _categoriaIngresoRepository.GetActive();

            var subCategoriasIngresoList = await _subCategoriaIngresoRepository.GetActiveByUser(idUser);

            var subCategoriasIngresoDto = new List<SubCategoriaIngresoDto>();

            if (subCategoriasIngresoList.Count() > 0)
            {
                subCategoriasIngresoDto = subCategoriasIngresoList.Select(s => _mapper.Map<SubCategoriaIngresoDto>(s)).ToList();
            }

            var categoriaYSubCategoriaIngresoDto = new List<CategoriaIngresoYSubCategoriasIngresoDto>();

            foreach (var categoria in categoriasIngresoList)
            {
                var subcategorias = new List<SubCategoriaIngresoDto>();

                if (subCategoriasIngresoDto.Count > 0)
                {
                    subcategorias = subCategoriasIngresoDto.Where(s => s.CategoriaIngresoId == categoria.Id).ToList();
                }

                var dto = new CategoriaIngresoYSubCategoriasIngresoDto()
                {
                    Id = categoria.Id,
                    Descripcion = categoria.Descripcion,
                    SubCategorias = subcategorias
                };

                categoriaYSubCategoriaIngresoDto.Add(dto);
            }

            return categoriaYSubCategoriaIngresoDto;
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
                                                            id != s.Id &&
                                                            s.Baja == false).Count() > 0)
            {
                Errors.Add("Descripcion", "La SubCategoria ya existe");
                flag = false;
            }

            var categoria = await _categoriaIngresoRepository.GetActiveById(insertUpdateDto.CategoriaIngresoId);
            if (categoria == null)
            {
                Errors.Add("Categoria", "La Categoria Ingreso no existe");
                flag = false;
            }

            var user = await _usuarioRepository.GetActiveById(insertUpdateDto.UsuarioId);
            if (user == null)
            {
                Errors.Add("Usuario", "El Usuario no existe");
                flag = false;
            }

            return flag;
        }

        public async Task<bool> Validate(InsertUpdateSubCategoriaIngresoDto insertUpdateDto)
        {
            bool flag = true;
            if (_subCategoriaIngresoRepository.Search(s => s.Descripcion == insertUpdateDto.Descripcion && s.Baja == false).Count() > 0)
            {
                Errors.Add("Descripcion", "La SubCategoria ya existe");
                flag = false;
            }

            var categoria = await _categoriaIngresoRepository.GetActiveById(insertUpdateDto.CategoriaIngresoId);
            if (categoria == null)
            {
                Errors.Add("Categoria", "La Categoria Ingreso no existe");
                flag = false;
            }

            var user = await _usuarioRepository.GetActiveById(insertUpdateDto.UsuarioId);
            if (user == null)
            {
                Errors.Add("Usuario", "El Usuario no existe");
                flag = false;
            }

            return flag;
        }

   
    }
}
