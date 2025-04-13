using BackendGastos.Service.DTOs.CategoriaGasto;
using BackendGastos.Service.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendGastos.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaGastoController : ControllerBase
    {

        private readonly ICategoriaGastoService _categoriaGastoService;

        public CategoriaGastoController(ICategoriaGastoService categoriaGastoService)
        {
            _categoriaGastoService = categoriaGastoService;
        }

        // GET: api/<CategoriaGastoController>
        [HttpGet]
        public async Task<IEnumerable<CategoriaGastoDto>> Get()
            => await _categoriaGastoService.Get();


        // GET api/<CategoriaGastoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaGastoDto>> Get(long id)
        {
            var categoriaGastoDto = await _categoriaGastoService.GetById(id);

            return categoriaGastoDto == null ? NotFound() : Ok(categoriaGastoDto);
        }

        // POST api/<CategoriaGastoController>
        [HttpPost]
        public async Task<ActionResult<CategoriaGastoDto>> Add(InsertUpdateCategoriaGastoDto insertUpdateCategoriaGasto)
        {
            if (!_categoriaGastoService.Validate(insertUpdateCategoriaGasto))
            {
                return BadRequest(_categoriaGastoService.Errors);
            }

            var categoriaGastoDto = await _categoriaGastoService.Add(insertUpdateCategoriaGasto);

            return CreatedAtAction(nameof(Get), new { id = categoriaGastoDto.Id }, categoriaGastoDto);
        }

        // PUT api/<CategoriaGastoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaGastoDto>> Put(long id, InsertUpdateCategoriaGastoDto insertUpdateCategoriaGasto)
        {
            if (!_categoriaGastoService.Validate(insertUpdateCategoriaGasto, id))
            {
                return BadRequest(_categoriaGastoService.Errors);
            }

            var categoriaGastoDto = await _categoriaGastoService.Update(id, insertUpdateCategoriaGasto);

            return categoriaGastoDto == null ? NotFound() : Ok(categoriaGastoDto);
        }

        // DELETE api/<CategoriaGastoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaGastoDto>> Delete(long id)
        {
            var categoriaGastoDto = await _categoriaGastoService.Delete(id);

            return categoriaGastoDto == null ? NotFound() : Ok(categoriaGastoDto);
        }
    }
}
