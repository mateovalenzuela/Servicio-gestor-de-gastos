using BackendGastos.Service.DTOs.Reporte;
using BackendGastos.Service.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendGastos.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;
        private readonly IValidator<ObtenerReporteDto> _obtenerReporteValidator;
        private readonly IUtilidadesService _utilidadesService;

        public ReporteController(IReporteService reporteService, IValidator<ObtenerReporteDto> obtenerReporteValidator, IUtilidadesService utilidadesService)
        {
            _reporteService = reporteService;
            _obtenerReporteValidator = obtenerReporteValidator;
            _utilidadesService = utilidadesService;
        }


        // GET api/<ReporteController>/totalGastosEIngresos/5
        [HttpGet("totalGastosEIngresos/{idUser}")]
        public async Task<ActionResult<ImportesDto>> GetImporteTotalDeGastosEIngresos(long idUser)
        {
            var importes = await _reporteService.GetImporteTotalDeGastosEIngresos(idUser);
            return importes == null ? NotFound() : Ok(importes);
        }

        //GET api/<ReporteController>/total-gastos-e-ingresos-by-date/5
        [HttpGet("totalGastosEIngresos/usuario/{idUser}/fechaLimite/{fechaLimite}/fechaInicial/{fechaInicial}")]
        public async Task<ActionResult<ImportesDto>> GetImporteTotalDeGastosEIngresos(long idUser, DateTime fechaLimite, DateTime fechaInicial)
        {

            var validDateFinal = _utilidadesService.ValidateDateTime(fechaLimite.ToString());
            var validDateInicial = _utilidadesService.ValidateDateTime(fechaInicial.ToString());

            if (validDateFinal is null)
            {
                return BadRequest("la Fecha final es invalida");
            }
            if (validDateInicial is null)
            {
                return BadRequest("la Fecha inicial es invalida");
            }

            var importes = await _reporteService.GetImporteTotalDeGastosEIngresos(idUser, fechaLimite, fechaInicial);
            return importes == null ? NotFound() : Ok(importes);
        }

        //GET api/<ReporteController>/balanceDiario/usuario/{idUser}/fechaLimite/{fechaLimite}/fechaInicial/{fechaInicial}
        [HttpGet("balanceDiario/usuario/{idUser}/fechaLimite/{fechaLimite}/fechaInicial/{fechaInicial}")]
        public async Task<ActionResult<List<BalanceDiarioDto>>> GetBalanceDiarioPorUsuario(long idUser, DateTime fechaLimite, DateTime fechaInicial)
        {
            var validDateLimite = _utilidadesService.ValidateDateTime(fechaLimite.ToString());
            var validDateInicial = _utilidadesService.ValidateDateTime(fechaInicial.ToString());

            if (validDateLimite == null)
            {
                return BadRequest("la Fecha final es invalida");
            }
            if (validDateInicial == null)
            {
                return BadRequest("la Fecha inicial es invalida");
            }

            var importes = await _reporteService.GetBalanceDiarioPorUsuario(idUser, fechaLimite, fechaInicial);
            return importes == null ? NotFound() : Ok(importes);
        }
    }
}
