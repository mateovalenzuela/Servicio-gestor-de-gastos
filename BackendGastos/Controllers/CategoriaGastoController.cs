using Microsoft.AspNetCore.Mvc;
using BackendGastos.Service.DTOs.CategoriaGasto;
using BackendGastos.Service.Services;
using FluentValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendGastos.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaGastoController : ControllerBase
    {

        private readonly IValidator<CategoriaGastoDto> _categoriaGastoValidator;
        private readonly IValidator<InsertUpdateCategoriaGastoDto> _insertUpdateCategoriaGastoValidator;
        private readonly ICommonService<CategoriaGastoDto, InsertUpdateCategoriaGastoDto> _categoriaGastoService;

        public CategoriaGastoController(IValidator<CategoriaGastoDto> categoriaGastoValidator, 
            IValidator<InsertUpdateCategoriaGastoDto> insertUpdateCategoriaGastoValidator, 
            ICommonService<CategoriaGastoDto, InsertUpdateCategoriaGastoDto> categoriaGastoService)
        {
            _categoriaGastoValidator = categoriaGastoValidator;
            _insertUpdateCategoriaGastoValidator = insertUpdateCategoriaGastoValidator;
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

            return categoriaGastoDto == null? NotFound() : Ok(categoriaGastoDto);
        }

        // POST api/<CategoriaGastoController>
        [HttpPost]
        public async Task<ActionResult<CategoriaGastoDto>> Add(InsertUpdateCategoriaGastoDto insertUpdateCategoriaGasto)
        {
            var validationResult = await _insertUpdateCategoriaGastoValidator.ValidateAsync(insertUpdateCategoriaGasto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_categoriaGastoService.Validate(insertUpdateCategoriaGasto))
            {
                return BadRequest(_categoriaGastoService.Errors);
            }

            var categoriaGastoDto = await _categoriaGastoService.Add(insertUpdateCategoriaGasto);

            return CreatedAtAction(nameof(Get), new {id = categoriaGastoDto.Id}, categoriaGastoDto);
        }

        // PUT api/<CategoriaGastoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaGastoDto>> Put(long id, InsertUpdateCategoriaGastoDto insertUpdateCategoriaGasto)
        {
            var validationResult = await _insertUpdateCategoriaGastoValidator.ValidateAsync(insertUpdateCategoriaGasto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_categoriaGastoService.Validate(insertUpdateCategoriaGasto, id))
            {
                return BadRequest(_categoriaGastoService.Errors);
            }

            var categoriaGastoDto = await _categoriaGastoService.Update(id, insertUpdateCategoriaGasto);

            return categoriaGastoDto == null? NotFound() : Ok(categoriaGastoDto);
        }

        // DELETE api/<CategoriaGastoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var categoriaGastoDto = await _categoriaGastoService.Delete(id);

            return categoriaGastoDto == null ? NotFound() : Ok(categoriaGastoDto);
        }
    }
}
