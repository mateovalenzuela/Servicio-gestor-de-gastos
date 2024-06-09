using AutoMapper;
using BackendGastos.Repository.Models;
using BackendGastos.Repository.Repository;
using BackendGastos.Service.DTOs.SubCategoriaGasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public class SubCategoriaGastoService : ISubCategoriaGastoService
    {
        public Dictionary<string, string> Errors { get; }
        private readonly ISubCategoriaGastoRepository _subCategoriaGastoRepository;
        private readonly IMapper _mapper;

        private readonly ICategoriaGastoRepository _categoriaGastoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SubCategoriaGastoService(ISubCategoriaGastoRepository subCategoriaGastoRepository, 
            IMapper mapper, 
            ICategoriaGastoRepository categoriaGastoRepository, 
            IUsuarioRepository usuarioRepository)
        {
            Errors = [];
            _subCategoriaGastoRepository = subCategoriaGastoRepository;
            _mapper = mapper;
            _categoriaGastoRepository = categoriaGastoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<SubCategoriaGastoDto> Add(InsertUpdateSubCategoriaGastoDto insertUpdateDto)
        {
            var subCategoriaGasto = _mapper.Map<GastosSubcategoriagasto>(insertUpdateDto);
            subCategoriaGasto.FechaCreacion = DateTime.UtcNow;

            subCategoriaGasto.CategoriaGasto = await _categoriaGastoRepository.GetActiveById(insertUpdateDto.CategoriaGastoId);
            subCategoriaGasto.Usuario = await _usuarioRepository.GetActiveById(insertUpdateDto.UsuarioId);

            await _subCategoriaGastoRepository.Add(subCategoriaGasto);
            await _subCategoriaGastoRepository.Save();

            var subCategoriaGastoDto = _mapper.Map<SubCategoriaGastoDto>(subCategoriaGasto);
            return subCategoriaGastoDto;
        }

        public async Task<SubCategoriaGastoDto?> Delete(long id)
        {
            var subCategoriaGasto = await _subCategoriaGastoRepository.GetActiveById(id);

            if (subCategoriaGasto != null)
            {
                _subCategoriaGastoRepository.BajaLogica(subCategoriaGasto);
                await _subCategoriaGastoRepository.Save();

                var subCategoriaGastoDto = _mapper.Map<SubCategoriaGastoDto>(subCategoriaGasto);
                return subCategoriaGastoDto;
            }

            return null;
        }

        public async Task<IEnumerable<SubCategoriaGastoDto>> Get()
        {
            var subCategoriasGasto = await _subCategoriaGastoRepository.GetActive();

            var subCategoriasGastoDto = subCategoriasGasto.Select(s => _mapper.Map<SubCategoriaGastoDto>(s)).ToList();

            return subCategoriasGastoDto;
        }

        public async Task<IEnumerable<SubCategoriaGastoDto>> GetActiveByCategoriaGasto(long idSubCategoriaGasto)
        {
            var subCategoriaGastoList = await _subCategoriaGastoRepository.GetActiveByCategoriaGasto(idSubCategoriaGasto);

            if (subCategoriaGastoList.Count() > 0)
            {
                var subCategoriaGastoDto = subCategoriaGastoList.Select(s => _mapper.Map<SubCategoriaGastoDto>(s)).ToList();
                return subCategoriaGastoDto;
            }

            return null;
        }

        public async Task<IEnumerable<SubCategoriaGastoDto>> GetActiveByUser(long idUser)
        {
            var subCategoriasGastoList = await _subCategoriaGastoRepository.GetActiveByUser(idUser);

            if (subCategoriasGastoList.Count() > 0)
            {
                var subCategoriasGastoDto = subCategoriasGastoList.Select(s => _mapper.Map<SubCategoriaGastoDto>(s)).ToList();
                return subCategoriasGastoDto;
            }

            return null;
        }

        public async Task<IEnumerable<SubCategoriaGastoDto>> GetActiveByUserAndCategoriaGasto(long idUser, long idSubCategoriaGasto)
        {
            var subCategoriasGastoList = await _subCategoriaGastoRepository.GetActiveByUserAndCategoriaGasto(idUser, idSubCategoriaGasto);

            if (subCategoriasGastoList.Count() > 0)
            {
                var subCategoriaGastoDto = subCategoriasGastoList.Select(s => _mapper.Map<SubCategoriaGastoDto>(s)).ToList();
                return subCategoriaGastoDto;
            }

            return null;
        }

        public async Task<SubCategoriaGastoDto?> GetById(long id)
        {
            var subCategoriaGasto = await _subCategoriaGastoRepository.GetActiveById(id);

            if (subCategoriaGasto != null)
            {
                var subCategoriaGastoDto = _mapper.Map<SubCategoriaGastoDto>(subCategoriaGasto);
                return subCategoriaGastoDto;
            }

            return null;
        }

        public async Task<SubCategoriaGastoDto?> Update(long id, InsertUpdateSubCategoriaGastoDto insertUpdateDto)
        {
            var subCategoriaGasto = await _subCategoriaGastoRepository.GetActiveById(id);

            if (subCategoriaGasto != null)
            {
                subCategoriaGasto.Descripcion = insertUpdateDto.Descripcion;
                subCategoriaGasto.CategoriaGastoId = insertUpdateDto.CategoriaGastoId;
                await _subCategoriaGastoRepository.Save();

                var subCategoriaGastoDto = _mapper.Map<SubCategoriaGastoDto>(subCategoriaGasto);
                return subCategoriaGastoDto;
            }

            return null;
        }

        public async Task<bool> Validate(InsertUpdateSubCategoriaGastoDto insertUpdateDto, long id)
        {
            bool flag = true;
            if (_subCategoriaGastoRepository.Search(s => s.Descripcion == insertUpdateDto.Descripcion &&
                                                            id != s.Id).Count() > 0)
            {
                Errors.Add("Subcategoria", "La SubCategoria ya existe");
                flag = false;
            }

            var categoria = await _categoriaGastoRepository.GetActiveById(insertUpdateDto.CategoriaGastoId);
            if (categoria == null)
            {
                Errors.Add("Categoria", "La Categoria Gasto no existe");
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

        public async Task<bool> Validate(InsertUpdateSubCategoriaGastoDto insertUpdateDto)
        {
            bool flag = true;
            if (_subCategoriaGastoRepository.Search(s => s.Descripcion == insertUpdateDto.Descripcion).Count() > 0)
            {
                Errors.Add("Descripcion", "La SubCategoria ya existe");
                flag = false;
            }

            var categoria = await _categoriaGastoRepository.GetActiveById(insertUpdateDto.CategoriaGastoId);
            if (categoria == null)
            {
                Errors.Add("Categoria", "La Categoria Gasto no existe");
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
