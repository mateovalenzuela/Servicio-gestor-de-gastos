using BackendGastos.Service.DTOs.Gasto;
using BackendGastos.Service.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendGastos.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastoController : ControllerBase
    {
        private readonly IGastoService _gastoService;

        public GastoController(IGastoService gastoService)
        {
            _gastoService = gastoService;
        }


        // GET: api/<GastoController>
        [HttpGet]
        public async Task<IEnumerable<GastoDto>> Get()
            => await _gastoService.Get();

        // GET api/<GastoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GastoDto>> Get(long id)
        {
            var gastoDto = await _gastoService.GetById(id);
            return gastoDto == null ? NotFound() : Ok(gastoDto);
        }

        // GET api/<GastoController>/usuario/5
        [HttpGet("usuario/{idUser}")]
        public async Task<ActionResult<GastoDto>> GetByUser(long idUser)
        {
            var gastosDto = await _gastoService.GetByUserId(idUser);
            return gastosDto == null ? NotFound() : Ok(gastosDto);
        }

        // GET api/<GastoController>/usuario/5/descripcion/comida
        [HttpGet("usuario/{idUser}/descripcion/{descripcion}")]
        public async Task<ActionResult<GastoDto>> SearchByDescripcionParcial(long idUser, string descripcion)
        {
            var gastosDto = await _gastoService.SearchByDescripcionParcial(idUser, descripcion);
            return gastosDto == null ? NotFound() : Ok(gastosDto);
        }


        // GET api/<GastoController>/categoriaGasto/5
        [HttpGet("categoriaGasto/{idCategoriaGasto}")]
        public async Task<ActionResult<GastoDto>> GetByCategoriaGasto(long idCategoriaGasto)
        {
            var gastosDto = await _gastoService.GetByCategoriaGastoId(idCategoriaGasto);
            return gastosDto == null ? NotFound() : Ok(gastosDto);
        }

        // GET api/<GastoController>/subcategoriaGasto/5
        [HttpGet("subcategoriaGasto/{idSubCategoriaGasto}")]
        public async Task<ActionResult<GastoDto>> GetBySubCategoriaGasto(long idSubCategoriaGasto)
        {
            var gastosDto = await _gastoService.GetBySubCategoriaGastoId(idSubCategoriaGasto);
            return gastosDto == null ? NotFound() : Ok(gastosDto);
        }

        // GET api/<GastoController>/usuario/5/categoriaGasto/5
        [HttpGet("usuario/{idUser}/categoriaGasto/{idCategoriaGasto}")]
        public async Task<ActionResult<GastoDto>> GetByUserAndCategoriaGasto(long idUser, long idCategoriaGasto)
        {
            var gastosDto = await _gastoService.GetByUserAndCategoriaGasto(idUser, idCategoriaGasto);
            return gastosDto == null ? NotFound() : Ok(gastosDto);
        }

        // POST api/<GastoController>
        [HttpPost]
        public async Task<ActionResult<GastoDto>> Add(InsertUpdateGastoDto insertUpdateGastoDto)
        {
            if (!await _gastoService.Validate(insertUpdateGastoDto))
            {
                return BadRequest(_gastoService.Errors);
            }

            var gastoDto = await _gastoService.Add(insertUpdateGastoDto);
            return CreatedAtAction(nameof(Get), new { Id = gastoDto.Id }, gastoDto);
        }

        // PUT api/<GastoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GastoDto>> Put(long id, InsertUpdateGastoDto insertUpdateGastoDto)
        {
            if (!await _gastoService.Validate(insertUpdateGastoDto))
            {
                return BadRequest(_gastoService.Errors);
            }

            var gastoDto = await _gastoService.Update(id, insertUpdateGastoDto);
            return gastoDto == null ? NotFound() : Ok(gastoDto);
        }

        // DELETE api/<GastoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GastoDto>> Delete(long id)
        {
            var gastoDto = await _gastoService.Delete(id);

            return gastoDto == null ? NotFound() : Ok(gastoDto);
        }
    }
}
