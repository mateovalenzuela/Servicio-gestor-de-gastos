using AutoMapper;
using BackendGastos.Repository.Repository;
using BackendGastos.Service.DTOs.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.Services
{
    public class ReporteService : IReporteService
    {
        private IReporteRepository _repository;
        private IUsuarioRepository _usuarioRepository;

        public ReporteService(IReporteRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<BalanceDiarioDto>> GetBalanceDiarioPorUsuario(long idUser, DateTime fechaLimite, DateTime fechaInicial)
        {
            var userExist = await _usuarioRepository.GetActiveById(idUser);
            if (userExist is null) return null;

            var balancesDiarios = await _repository.GetBalanceDiarioPorUsuario(idUser, fechaLimite, fechaInicial);

            List<BalanceDiarioDto> listaDeBalancesDiarios = [];

            foreach (var balanceDiario in balancesDiarios)
            {
                BalanceDiarioDto balanceDto = new()
                {
                    Fecha = balanceDiario.Fecha,
                    Importe = balanceDiario.Balance
                };
                listaDeBalancesDiarios.Add(balanceDto);
            }
            return listaDeBalancesDiarios;
        }

        public async Task<ImportesDto> GetImporteTotalDeGastosEIngresos(long idUser)
        {
            var userExist = await _usuarioRepository.GetActiveById(idUser);
            var importes = await _repository.GetImporteTotalDeGastosEIngresos(idUser);

            if (userExist is null) return null;

            if (importes.Count != 0)
            {
                ImportesDto importesDto = new()
                {
                    ImporteGastos = importes["ImporteGastos"],
                    ImporteIngresos = importes["ImporteIngresos"],
                    ImportesTotales = importes["ImporteBalance"]
                };
                return importesDto;
            }
            return null;
        }

        public async Task<ImportesDto> GetImporteTotalDeGastosEIngresos(long idUser, DateTime fechaLimite, DateTime fechaInicial)
        {
            var userExist = await _usuarioRepository.GetActiveById(idUser);
            if (userExist is null) return null;

            var importes = await _repository.GetImporteTotalDeGastosEIngresos(idUser, fechaLimite, fechaInicial);
            if (importes.Count != 0)
            {
                ImportesDto importesDto = new()
                {
                    ImporteGastos = importes["ImporteGastos"],
                    ImporteIngresos = importes["ImporteIngresos"],
                    ImportesTotales = importes["ImporteBalance"]
                };
                return importesDto;
            }
            return null;
        }
    }
}
