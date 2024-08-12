using AutoMapper;
using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.CategoriaGasto;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Service.DTOs.Gasto;
using BackendGastos.Service.DTOs.Ingreso;
using BackendGastos.Service.DTOs.SubCategoriaGasto;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CategoriaIngreso
            CreateMap<InsertUpdateCategoriaIngresoDto, GastosCategoriaigreso>();
            CreateMap<GastosCategoriaigreso, CategoriaIngresoDto>();
            // CategoriaGasto
            CreateMap<InsertUpdateCategoriaGastoDto, GastosCategoriagasto>();
            CreateMap<GastosCategoriagasto, CategoriaGastoDto>();
            // SubCategoriaIngreso
            CreateMap<InsertUpdateSubCategoriaIngresoDto, GastosSubcategoriaingreso>();
            CreateMap<GastosSubcategoriaingreso, SubCategoriaIngresoDto>();
            // SubCategoriaGasto
            CreateMap<InsertUpdateSubCategoriaGastoDto, GastosSubcategoriagasto>();
            CreateMap<GastosSubcategoriagasto, SubCategoriaGastoDto>();
            // Ingreso
            CreateMap<InsertUpdateIngresoDto, GastosIngreso>();
            CreateMap<GastosIngreso, IngresoDto>()
                .ForMember(dest => dest.CategoriaIngresoDescripcion, opt => opt.MapFrom(src => src.CategoriaIngreso.Descripcion))
                .ForMember(dest => dest.SubcategoriaIngresoDescripcion, opt => opt.MapFrom(src => src.SubcategoriaIngreso.Descripcion))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion.ToString("dd-MM-yyyy HH:mm:ss")));
            // Gasto
            CreateMap<InsertUpdateGastoDto, GastosGasto>();
            CreateMap<GastosGasto, GastoDto>()
                .ForMember(dest => dest.CategoriaGastoDescripcion, opt => opt.MapFrom(src => src.CategoriaGasto.Descripcion))
                .ForMember(dest => dest.SubCategoriaGastoDescripcion, opt => opt.MapFrom(src => src.SubcategoriaGasto.Descripcion))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion.ToString("dd-MM-yyyy HH:mm:ss")));
        }
    }
}
