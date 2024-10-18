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
    public class SubCategoriaIngresoController : ControllerBase
    {

        private readonly IValidator<SubCategoriaIngresoDto> _subCategoriaIngresoValidator;
        private readonly IValidator<InsertUpdateSubCategoriaIngresoDto> _insertUpdateSubCategoriaIngresoValidator;
        private readonly ISubCategoriaIngresoService _subCategoriaIngresoService;

        public SubCategoriaIngresoController(IValidator<SubCategoriaIngresoDto> subCategoriaIngresoValidator, 
            IValidator<InsertUpdateSubCategoriaIngresoDto> insertUpdateSubCategoriaIngresoValidator, 
            ISubCategoriaIngresoService subCategoriaIngresoService)
        {
            _subCategoriaIngresoValidator = subCategoriaIngresoValidator;
            _insertUpdateSubCategoriaIngresoValidator = insertUpdateSubCategoriaIngresoValidator;
            _subCategoriaIngresoService = subCategoriaIngresoService;
        }




        // GET: api/<SubCategoriaIngresoController>
        [HttpGet]
        public async Task<IEnumerable<SubCategoriaIngresoDto>> Get()
            => await _subCategoriaIngresoService.Get();

        // GET api/<SubCategoriaIngresoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoriaIngresoDto>> Get(long id) 
        {
            var subCategoriaIngresoDto = await _subCategoriaIngresoService.GetById(id);

            return subCategoriaIngresoDto == null ? NotFound() : Ok(subCategoriaIngresoDto);
        }

        // GET api/<SubCategoriaIngresoController>/usuario/5
        [HttpGet("usuario/{idUser}")]
        public async Task<ActionResult<SubCategoriaIngresoDto>> GetByUser(long idUser)
        {
            var subCategoriaIngresoDto = await _subCategoriaIngresoService.GetActiveByUser(idUser);

            return subCategoriaIngresoDto == null ? NotFound() : Ok(subCategoriaIngresoDto);
        }

        // GET api/<SubCategoriaIngresoController>/groupByCategoriaIngresoByUsuario/5
        [HttpGet("groupByCategoriaIngresoByUsuario/{idUser}")]
        public async Task<ActionResult<CategoriaIngresoYSubCategoriasIngresoDto>> GetGroupByCategoriaIngresoByUser(long idUser)
        {
            var categoriaYSubCategoriaIngresoDto = await _subCategoriaIngresoService.GetActiveGroupByCategoriaIngresoByUser(idUser);

            return categoriaYSubCategoriaIngresoDto.Count() == 0 ? NotFound() : Ok(categoriaYSubCategoriaIngresoDto);
        }


        // GET api/<SubCategoriaIngresoController>/groupByCategoriaIngresoWithAmountByUsuario/5
        [HttpGet("groupByCategoriaIngresoWithAmountByUsuario/{idUser}")]
        public async Task<ActionResult<CategoriaIngresoYSubCategoriasIngresoDto>> GetGroupByCategoriaIngresoWithAmountByUser(long idUser)
        {
            var categoriaYSubCategoriaIngresoWithAmountDto = await _subCategoriaIngresoService.GetActiveGroupByCategoriaIngresoWithAmountByUser(idUser);

            return categoriaYSubCategoriaIngresoWithAmountDto.Count() == 0 ? NotFound() : Ok(categoriaYSubCategoriaIngresoWithAmountDto);
        }

        // GET api/<SubCategoriaIngresoController>/categoriaIngreso/5
        [HttpGet("categoriaIngreso/{idCategoriaIngreso}")]
        public async Task<ActionResult<SubCategoriaIngresoDto>> GetByCategoriaIngreso(long idCategoriaIngreso)
        {
            var subCategoriaIngresoDto = await _subCategoriaIngresoService.GetActiveByCategoriaIngreso(idCategoriaIngreso);

            return subCategoriaIngresoDto == null ? NotFound() : Ok(subCategoriaIngresoDto);
        }

        // GET api/<SubCategoriaIngresoController>/usuario/5/categoriaIngreso/5
        [HttpGet("usuario/{idUser}/categoriaIngreso/{idCategoriaIngreso}")]
        public async Task<ActionResult<SubCategoriaIngresoDto>> GetByUserAndCategoriaIngreso(long idUser, long idCategoriaIngreso)
        {
            var subCategoriaIngresoDto = await _subCategoriaIngresoService.GetActiveByUserAndCategoriaIngreso(idUser, idCategoriaIngreso);

            return subCategoriaIngresoDto == null ? NotFound() : Ok(subCategoriaIngresoDto);
        }

        // POST api/<SubCategoriaIngresoController>
        [HttpPost]
        public async Task<ActionResult<SubCategoriaIngresoDto>> Add(InsertUpdateSubCategoriaIngresoDto insertUpadateSubCategoriaIngresoDto)
        {
            var validationResult = await _insertUpdateSubCategoriaIngresoValidator.ValidateAsync(insertUpadateSubCategoriaIngresoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!await _subCategoriaIngresoService.Validate(insertUpadateSubCategoriaIngresoDto))
            {
                return BadRequest(_subCategoriaIngresoService.Errors);
            }

            var subCategoriaIngresoDto = await _subCategoriaIngresoService.Add(insertUpadateSubCategoriaIngresoDto);

            return CreatedAtAction(nameof(Get), new { id = subCategoriaIngresoDto.Id }, subCategoriaIngresoDto);
        }
        

        // PUT api/<SubCategoriaIngresoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SubCategoriaIngresoDto>> Put(long id, InsertUpdateSubCategoriaIngresoDto insertUpdateSubCategoriaIngresoDto)
        {
            var validationResult = await _insertUpdateSubCategoriaIngresoValidator.ValidateAsync(insertUpdateSubCategoriaIngresoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!await _subCategoriaIngresoService.Validate(insertUpdateSubCategoriaIngresoDto))
            {
                return BadRequest(_subCategoriaIngresoService.Errors);
            }

            var subCategoriaIngresoDto = await _subCategoriaIngresoService.Update(id, insertUpdateSubCategoriaIngresoDto);

            return subCategoriaIngresoDto == null ? NotFound() : Ok(subCategoriaIngresoDto);
        }

        // DELETE api/<SubCategoriaIngresoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SubCategoriaIngresoDto>> Delete(long id)
        {
            var subCategoriaIngresoDto = await _subCategoriaIngresoService.Delete(id);

            return subCategoriaIngresoDto == null ? NotFound() : Ok(subCategoriaIngresoDto);
        }
    }
}
