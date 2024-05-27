using Microsoft.AspNetCore.Mvc;
using BackendGastos.Service.DTOs.CategoriaIngreso;
using BackendGastos.Validator.CategoriaIngreso;
using BackendGastos.Service.Services;
using FluentValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaIngresoController : ControllerBase
    {

        private readonly IValidator<CategoriaIngresoDto> _categoriaIngresoValidator;
        private readonly IValidator<InsertUpdateCategoriaIngresoDto> _insertUpdateCategoriaIngresoValidator;
        private readonly ICategoriaIngresoService _categoriaIngresoService;

        public CategoriaIngresoController(IValidator<CategoriaIngresoDto> categoriaIngresoValidator,
            IValidator<InsertUpdateCategoriaIngresoDto> insertUpdateCategoriaIngresoValidator,
            ICategoriaIngresoService categoriaIngresoService)
        {
            _categoriaIngresoValidator = categoriaIngresoValidator;
            _insertUpdateCategoriaIngresoValidator = insertUpdateCategoriaIngresoValidator;
            _categoriaIngresoService = categoriaIngresoService;
        }

        // GET: api/<CategoriaIngresoController>
        [HttpGet]
        public async Task<IEnumerable<CategoriaIngresoDto>> Get()
            => await _categoriaIngresoService.Get();

        // GET api/<CategoriaIngresoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaIngresoDto>> Get(long id)
        {
            var categoriaIngresoDto = await _categoriaIngresoService.GetById(id);

            return categoriaIngresoDto == null ? NotFound() : Ok(categoriaIngresoDto);               
        }

        // POST api/<CategoriaIngresoController>
        [HttpPost]
        public async Task<ActionResult<CategoriaIngresoDto>> Add(InsertUpdateCategoriaIngresoDto insertCategoriaIngresoDto)
        {
            var validationResult = await _insertUpdateCategoriaIngresoValidator.ValidateAsync(insertCategoriaIngresoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_categoriaIngresoService.Validate(insertCategoriaIngresoDto))
            {
                return BadRequest(_categoriaIngresoService.Errors);
            }

            var categoriaIngresoDto = await _categoriaIngresoService.Add(insertCategoriaIngresoDto);

            return CreatedAtAction(nameof(Get), new {id = categoriaIngresoDto.Id}, categoriaIngresoDto);
        }

        // PUT api/<CategoriaIngresoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaIngresoDto>> Put(long id, InsertUpdateCategoriaIngresoDto insertCategoriaIngresoDto)
        {
            var validationResult = await _insertUpdateCategoriaIngresoValidator.ValidateAsync(insertCategoriaIngresoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_categoriaIngresoService.Validate(insertCategoriaIngresoDto, id))
            {
                return BadRequest(_categoriaIngresoService.Errors);
            }

            var categoriaIngresoDto = await _categoriaIngresoService.Update(id, insertCategoriaIngresoDto);  
            
            return categoriaIngresoDto == null? NotFound() : Ok(categoriaIngresoDto);
        }

        // DELETE api/<CategoriaIngresoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var categoriaIngresoDto = await _categoriaIngresoService.Delete(id);

            return categoriaIngresoDto == null ? NotFound() : Ok(categoriaIngresoDto);
        }
        
    }
        
}
