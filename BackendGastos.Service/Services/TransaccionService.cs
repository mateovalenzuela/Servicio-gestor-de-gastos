using AutoMapper;
using BackendGastos.Repository.Models;
using BackendGastos.Repository.Repository;
using BackendGastos.Service.DTOs.Gasto;
using BackendGastos.Service.DTOs.Ingreso;
using BackendGastos.Service.DTOs.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public class TransaccionService : ITransaccionService
    {
        ITransaccionRepository _repository;
        IMapper _mapper;

        public TransaccionService(ITransaccionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TransaccionDto>> GetGastosEIngresos(long idUser, int cantidad)
        {            
            if (cantidad <= 0)
            {
                cantidad = 0;
            }

            var transacciones = await _repository.GetActiveGastosEIngresos(idUser, cantidad);

            var transaccionesDtos = new List<TransaccionDto>();

            if (transacciones.Count() == 0)
            {
                return transaccionesDtos;
            }

            foreach(var transaccion in transacciones)
            {
                var ingreso = new IngresoDto();
                var gasto = new GastoDto();

                if (transaccion.Ingreso != null)
                {
                    ingreso = _mapper.Map<IngresoDto>(transaccion.Ingreso);
                    gasto = null;
                }
                else
                {
                    gasto = _mapper.Map<GastoDto>(transaccion.Gasto);
                    ingreso = null;
                }

                var transaccionDto = new TransaccionDto
                {
                    IsIngreso = transaccion.IsIngreso,
                    Ingreso = ingreso,
                    Gasto = gasto,
                };
                transaccionesDtos.Add(transaccionDto);
            }

            return transaccionesDtos;
        }

        public async Task<ImporteTransaccionDto> GetImportes(long idUser)
        {
            var importes = await _repository.GetImportesGastosEIngresos(idUser);

            var importeDto = new ImporteTransaccionDto
            {
                ImporteGastos = importes["ImporteGastos"],
                ImporteIngresos = importes["ImporteIngresos"],
                ImporteTotal = importes["ImporteTotal"],
            };
            return importeDto;
        }
    }
}
