using BackendGastos.Service.DTOs.Ingreso;
using BackendGastos.Service.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendGastos.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoController : ControllerBase
    {
        private readonly IValidator<IngresoDto> _ingresoValidator;
        private readonly IValidator<InsertUpdateIngresoDto> _insertUpdateIngresoValidator;
        private readonly IIngresoService _ingresoService;

        public IngresoController(IValidator<IngresoDto> ingresoValidator, 
            IValidator<InsertUpdateIngresoDto> insertUpdateIngresoValidator, 
            IIngresoService ingresoService)
        {
            _ingresoValidator = ingresoValidator;
            _insertUpdateIngresoValidator = insertUpdateIngresoValidator;
            _ingresoService = ingresoService;
        }




        // GET: api/<IngresoController>
        [HttpGet]
        public async Task<IEnumerable<IngresoDto>> Get() 
            => await _ingresoService.Get();

        // GET api/<IngresoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngresoDto>> Get(long id)
        {
            var ingresoDto = await _ingresoService.GetById(id);
            return ingresoDto == null ? NotFound() : Ok(ingresoDto);
        }


        // GET api/<IngresoController>/usuario/5
        [HttpGet("usuario/{idUser}")]
        public async Task<ActionResult<IngresoDto>> GetByUser(long idUser)
        {
            var ingresosDto = await _ingresoService.GetByUserId(idUser);
            return ingresosDto == null ? NotFound() : Ok(ingresosDto);
        }

        // GET api/<IngresoController>/categoriaIngreso/5
        [HttpGet("categoriaIngreso/{idCategoriaIngreso}")]
        public async Task<ActionResult<IngresoDto>> GetByCategoriaIngreso(long idCategoriaIngreso)
        {
            var ingresosDto = await _ingresoService.GetByCategoriaIngresoId(idCategoriaIngreso);
            return ingresosDto == null ? NotFound() : Ok(ingresosDto);
        }

        // GET api/<IngresoController>/subcategoriaIngreso/5
        [HttpGet("subcategoriaIngreso/{idSubCategoriaIngreso}")]
        public async Task<ActionResult<IngresoDto>> GetBySubCategoriaIngreso(long idSubCategoriaIngreso)
        {
            var ingresosDto = await _ingresoService.GetBySubCategoriaIngresoId(idSubCategoriaIngreso);
            return ingresosDto == null ? NotFound() : Ok(ingresosDto);
        }

        // GET api/<IngresoController>/usuario/5/categoriaIngreso/5
        [HttpGet("usuario/{idUser}/categoriaIngreso/{idCategoriaIngreso}")]
        public async Task<ActionResult<IngresoDto>> GetByUserAndCategoriaIngreso(long idUser, long idCategoriaIngreso)
        {
            var ingresosDto = await _ingresoService.GetByUserAndCategoriaIngreso(idUser, idCategoriaIngreso);
            return ingresosDto == null ? NotFound() : Ok(ingresosDto);
        }


        // POST api/<IngresoController>
        [HttpPost]
        public async Task<ActionResult<IngresoDto>> Add(InsertUpdateIngresoDto insertUpdateIngresoDto)
        {
            var validationResult = await _insertUpdateIngresoValidator.ValidateAsync(insertUpdateIngresoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (! await _ingresoService.Validate(insertUpdateIngresoDto))
            {
                return BadRequest(_ingresoService.Errors2);
            }

            var ingresoDto = await _ingresoService.Add(insertUpdateIngresoDto);
            return CreatedAtAction(nameof(Get), new {id = ingresoDto.Id}, ingresoDto);
        }

        // PUT api/<IngresoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IngresoDto>> Put(long id, InsertUpdateIngresoDto insertUpdateIngresoDto)
        {
            var validationResult = await _insertUpdateIngresoValidator.ValidateAsync(insertUpdateIngresoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!await _ingresoService.Validate(id,insertUpdateIngresoDto))
            {
                return BadRequest(_ingresoService.Errors);
            }

            var ingresoDto = await _ingresoService.Update(id, insertUpdateIngresoDto);
            return ingresoDto == null ? NotFound() : Ok(ingresoDto);
        }

        // DELETE api/<IngresoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IngresoDto>> Delete(long id)
        {
            var ingresoDto = await _ingresoService.Delete(id);

            return ingresoDto == null ? NotFound() : Ok(ingresoDto);
        }
    }
}
