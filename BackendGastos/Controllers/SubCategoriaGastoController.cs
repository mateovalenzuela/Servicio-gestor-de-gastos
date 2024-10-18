using BackendGastos.Service.DTOs.SubCategoriaGasto;
using BackendGastos.Service.DTOs.SubCategoriaIngreso;
using BackendGastos.Service.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendGastos.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriaGastoController : ControllerBase
    {

        private readonly IValidator<SubCategoriaGastoDto> _subCategoriaGastoValidator;
        private readonly IValidator<InsertUpdateSubCategoriaGastoDto> _insertUpdateSubCategoriaGastoValidator;
        private readonly ISubCategoriaGastoService _subCategoriaGastoService;

        public SubCategoriaGastoController(IValidator<SubCategoriaGastoDto> subCategoriaGastoValidator, 
            IValidator<InsertUpdateSubCategoriaGastoDto> insertUpdateSubCategoriaGastoValidator, 
            ISubCategoriaGastoService subCategoriaGastoService)
        {
            _subCategoriaGastoValidator = subCategoriaGastoValidator;
            _insertUpdateSubCategoriaGastoValidator = insertUpdateSubCategoriaGastoValidator;
            _subCategoriaGastoService = subCategoriaGastoService;
        }


        // GET: api/<SubCategoriaGastoController>
        [HttpGet]
        public async Task<IEnumerable<SubCategoriaGastoDto>> Get()
            => await _subCategoriaGastoService.Get();

        // GET api/<SubCategoriaGastoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoriaGastoDto>> Get(long id)
        {
            var subCategoriaGastoDto = await _subCategoriaGastoService.GetById(id);

            return subCategoriaGastoDto == null ? NotFound() : Ok(subCategoriaGastoDto);
        }


        // GET api/<SubCategoriaGastoController>/usuario/5
        [HttpGet("usuario/{idUser}")]
        public async Task<ActionResult<SubCategoriaGastoDto>> GetByUser(long idUser)
        {
            var subCategoriaGastoDto = await _subCategoriaGastoService.GetActiveByUser(idUser);

            return subCategoriaGastoDto == null ? NotFound() : Ok(subCategoriaGastoDto);
        }


        // GET api/<SubCategoriaGastoController>/groupByCategoriaGastoByUsuario/5
        [HttpGet("groupByCategoriaGastoByUsuario/{idUser}")]
        public async Task<ActionResult<CategoriaGastoYSubCategoriasGastoDto>> GetGroupByCategoriaGastoByUser(long idUser)
        {
            var categoriaYSubCategoriaGastoDto = await _subCategoriaGastoService.GetActiveGroupByCategoriaGastoByUser(idUser);

            return categoriaYSubCategoriaGastoDto.Count() == 0 ? NotFound() : Ok(categoriaYSubCategoriaGastoDto);
        }


        // GET api/<SubCategoriaGastoController>/groupByCategoriaGastoWithAmountByUsuario/5
        [HttpGet("groupByCategoriaGastoWithAmountByUsuario/{idUser}")]
        public async Task<ActionResult<CategoriaGastoYSubCategoriasGastoDto>> GetGroupByCategoriaGastoWithAmountByUser(long idUser)
        {
            var categoriaYSubCategoriaGastoWithAmountDto = await _subCategoriaGastoService.GetActiveGroupByCategoriaGastoWithAmountByUser(idUser);

            return categoriaYSubCategoriaGastoWithAmountDto.Count() == 0 ? NotFound() : Ok(categoriaYSubCategoriaGastoWithAmountDto);
        }


        // GET api/<SubCategoriaGastoController>/categoriaGasto/5
        [HttpGet("categoriaGasto/{idCategoriaGasto}")]
        public async Task<ActionResult<SubCategoriaGastoDto>> GetByCategoriaGasto(long idCategoriaGasto)
        {
            var subCategoriaGastoDto = await _subCategoriaGastoService.GetActiveByCategoriaGasto(idCategoriaGasto);

            return subCategoriaGastoDto == null ? NotFound() : Ok(subCategoriaGastoDto);
        }


        // GET api/<SubCategoriaGastoController>/usuario/5/categoriaGasto/5
        [HttpGet("usuario/{idUser}/categoriaGasto/{idCategoriaGasto}")]
        public async Task<ActionResult<SubCategoriaGastoDto>> GetByUserAndCategoriaGasto(long idUser, long idCategoriaGasto)
        {
            var subCategoriaGastoDto = await _subCategoriaGastoService.GetActiveByUserAndCategoriaGasto(idUser, idCategoriaGasto);

            return subCategoriaGastoDto == null ? NotFound() : Ok(subCategoriaGastoDto);
        }


        // POST api/<SubCategoriaGastoController>
        [HttpPost]
        public async Task<ActionResult<SubCategoriaGastoDto>> Add(InsertUpdateSubCategoriaGastoDto insertUpdateSubCategoriaGastoDto)
        {
            var validationResult = await _insertUpdateSubCategoriaGastoValidator.ValidateAsync(insertUpdateSubCategoriaGastoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!await _subCategoriaGastoService.Validate(insertUpdateSubCategoriaGastoDto))
            {
                return BadRequest(_subCategoriaGastoService.Errors);
            }

            var subCategoriaGastoDto = await _subCategoriaGastoService.Add(insertUpdateSubCategoriaGastoDto);

            return CreatedAtAction(nameof(Get), new { id = subCategoriaGastoDto.Id }, subCategoriaGastoDto);
        }

        // PUT api/<SubCategoriaGastoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SubCategoriaGastoDto>> Put(long id, InsertUpdateSubCategoriaGastoDto insertUpdateSubCategoriaGastoDto)
        {
            var validationResult = await _insertUpdateSubCategoriaGastoValidator.ValidateAsync(insertUpdateSubCategoriaGastoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!await _subCategoriaGastoService.Validate(insertUpdateSubCategoriaGastoDto))
            {
                return BadRequest(_subCategoriaGastoService.Errors);
            }

            var subCategoriaGastoDto = await _subCategoriaGastoService.Update(id, insertUpdateSubCategoriaGastoDto);

            return subCategoriaGastoDto == null ? NotFound() : Ok(subCategoriaGastoDto);
        }

        // DELETE api/<SubCategoriaGastoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SubCategoriaGastoDto>> Delete(long id)
        {
            var subCategoriaGastoDto = await _subCategoriaGastoService.Delete(id);

            return subCategoriaGastoDto == null ? NotFound() : Ok(subCategoriaGastoDto);
        }
    }
}
