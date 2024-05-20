using AutoMapper;
using BackendGastos.Repository.Models;
using BackendGastos.Service.DTOs.CategoriaIngreso;
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
        }
    }
}
