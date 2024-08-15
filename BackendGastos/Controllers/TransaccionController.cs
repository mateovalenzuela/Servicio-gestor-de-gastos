using BackendGastos.Service.DTOs.Gasto;
using BackendGastos.Service.DTOs.Transaccion;
using BackendGastos.Service.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendGastos.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private ITransaccionService _transaccionService;

        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        // GET api/<TransaccionController>/5
        [HttpGet("usuario/{idUser}/cantidad/{cantidad}")]
        public async Task<IEnumerable<TransaccionDto>> GetByUserAndCatidad(long idUser, int cantidad)
            => await _transaccionService.GetGastosEIngresos(idUser, cantidad);
            
    }
}
